using SDL2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GBEmu.Win
{
    public partial class GameView : Form
    {
        private const int scaleFactor = 4;

        private const int gbWidth = 160;
        private const int gbHeight = 144;

        private const int textWidth = 250;

        private const int screenWidth = gbWidth * scaleFactor;
        private const int screenHeight = gbHeight * scaleFactor;

        private Panel gamePanel;
        private IntPtr gameWindow;

        static IntPtr glRenderer;
        static IntPtr glTexture;

        static IntPtr glFont;

        static IntPtr textSurface;
        static IntPtr textTexture;

        static Size windowSize = new Size(screenWidth + textWidth, screenHeight);

        static SDL.SDL_Rect screenRect = new SDL.SDL_Rect()
        {
            x = 0,
            y = 0,
            w = screenWidth,
            h = screenHeight
        };

        static SDL.SDL_Rect textSrcRect;
        static SDL.SDL_Rect textDestRect;

        static byte[,,] pixels = new byte[gbHeight, gbWidth, 3];

        static Task renderTask;
        static CancellationTokenSource renderCancellationToken;

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowPos(
            IntPtr handle,
            IntPtr handleAfter,
            int x,
            int y,
            int cx,
            int cy,
            uint flags
        );
        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr child, IntPtr newParent);
        [DllImport("user32.dll")]
        private static extern IntPtr ShowWindow(IntPtr handle, int command);

        public GameView()
        {
            gamePanel = new Panel();
            gamePanel.Size = windowSize;
            gamePanel.Location = new Point(0, 0);

            ClientSize = windowSize;
            FormClosing += new FormClosingEventHandler(WindowClosing);

            Controls.Add(gamePanel);

            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);
            SDL_ttf.TTF_Init();

            gameWindow = SDL.SDL_CreateWindow("GBEmu",
                SDL.SDL_WINDOWPOS_CENTERED,
                SDL.SDL_WINDOWPOS_CENTERED,
                windowSize.Width,
                windowSize.Height,
                SDL.SDL_WindowFlags.SDL_WINDOW_BORDERLESS | SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL);

            glRenderer = SDL.SDL_CreateRenderer(gameWindow, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
            glFont = SDL_ttf.TTF_OpenFont("assets/arial.ttf", 15);

            glTexture = SDL.SDL_CreateTexture(glRenderer,
                SDL.SDL_PIXELFORMAT_RGB24,
                (int)SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING,
                gbWidth, gbHeight);

            // Get the Win32 HWND from the SDL2 window
            SDL.SDL_SysWMinfo info = new SDL.SDL_SysWMinfo();
            SDL.SDL_GetWindowWMInfo(gameWindow, ref info);
            IntPtr winHandle = info.info.win.window;

            SetWindowPos(
                winHandle,
                Handle,
                0,
                0,
                0,
                0,
                0x0401 // NOSIZE | SHOWWINDOW
            );

            // Attach the SDL2 window to the panel
            SetParent(winHandle, gamePanel.Handle);
            ShowWindow(winHandle, 1); // SHOWNORMAL

            //for (int y = 0; y < gbHeight; y++)
            //{
            //    for (int x = 0; x < gbWidth; x++)
            //    {
            //        pixels[y, x, 0] = (byte)(((float)x / gbWidth) * 255);
            //        pixels[y, x, 1] = (byte)(((float)x / gbWidth) * 255);
            //        pixels[y, x, 2] = (byte)(((float)x / gbWidth) * 255);
            //    }
            //}

            //UpdateText("Hello World!\nHello World!");

            renderCancellationToken = new CancellationTokenSource();

            renderTask = Task.Factory.StartNew(async () =>
            {
                DateTime now = DateTime.Now;
                DateTime lastExecution = now;

                TimeSpan elapsedTime = TimeSpan.FromSeconds(0);
                long count = 0;

                while (true)
                {
                    now = DateTime.Now;
                    elapsedTime = now - lastExecution;
                    lastExecution = now;

                    if(count % 10 == 1)
                    {
                        UpdateText("Elapsed: " + elapsedTime.TotalMilliseconds);
                    }

                    Render(elapsedTime.TotalMilliseconds);

                    count++;

                    await Task.Delay(30);
                }
            }, renderCancellationToken.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private void Render(double elapsedTime)
        {
            GCHandle handle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            IntPtr pixelsPtr = handle.AddrOfPinnedObject();

            SDL.SDL_UpdateTexture(glTexture, IntPtr.Zero, pixelsPtr, gbWidth * sizeof(byte) * 3);

            SDL.SDL_RenderClear(glRenderer);
            SDL.SDL_RenderCopy(glRenderer, glTexture, ref screenRect, ref screenRect);
            SDL.SDL_RenderCopy(glRenderer, textTexture, ref textSrcRect, ref textDestRect);

            SDL.SDL_RenderPresent(glRenderer);

            handle.Free();
        }

        private void UpdateText(string text)
        {
            SDL.SDL_Color color = new SDL.SDL_Color()
            {
                r = 255,
                g = 255,
                b = 255,
                a = 0,
            };

            textSurface = SDL_ttf.TTF_RenderText_Blended_Wrapped(glFont, text, color, textWidth);
            textTexture = SDL.SDL_CreateTextureFromSurface(glRenderer, textSurface);

            SDL_ttf.TTF_SizeText(glFont, text, out int actualWidth, out int actualHeight);

            actualHeight *= text.Count(x => x == '\n') + 1;

            textSrcRect = new SDL.SDL_Rect()
            {
                x = 0,
                y = 0,
                w = actualWidth,
                h = actualHeight
            };

            textDestRect = new SDL.SDL_Rect()
            {
                x = screenWidth,
                y = 0,
                w = actualWidth,
                h = actualHeight
            };
        }

        private void WindowClosing(object sender, FormClosingEventArgs e)
        {
            renderCancellationToken.Cancel();

            SDL.SDL_DestroyTexture(textTexture);
            SDL.SDL_FreeSurface(textSurface);

            SDL_ttf.TTF_CloseFont(glFont);

            SDL.SDL_DestroyWindow(gameWindow);
            gameWindow = IntPtr.Zero;

            SDL_ttf.TTF_Quit();
            SDL.SDL_Quit();
        }
    }
}
using SDL2;
using System;
using System.Runtime.InteropServices;

namespace GBEmu.GUI
{
    class Program
    {
        static IntPtr renderer;
        static IntPtr texture;

        static byte[,,] pixels = new byte[144, 160, 3];

        static void Main(string[] args)
        {
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);

            var window = SDL.SDL_CreateWindow("GBEmu",
                SDL.SDL_WINDOWPOS_CENTERED,
                SDL.SDL_WINDOWPOS_CENTERED,
                160,
                144,
                SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            renderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

            texture = SDL.SDL_CreateTexture(renderer,
                SDL.SDL_PIXELFORMAT_RGB24,
                (int)SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING,
                160, 144);

            for (int x = 0; x < 144; x++)
            {
                for (int y = 0; y < 160; y++)
                {
                    pixels[x, y, 0] = 0x00;
                    pixels[x, y, 1] = 0xDD;
                    pixels[x, y, 2] = 0xDD;
                }
            }


            bool quit = false;

            while (!quit)
            {
                while(SDL.SDL_PollEvent(out SDL.SDL_Event e) != 0)
                {
                    switch (e.type)
                    {
                        case SDL.SDL_EventType.SDL_QUIT:
                            quit = true;
                            break;
                    }
                }

                Render();
            }

            SDL.SDL_Quit();
        }

        static void Render()
        {
            GCHandle handle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            IntPtr pixelsPtr = handle.AddrOfPinnedObject();

            SDL.SDL_UpdateTexture(texture, IntPtr.Zero, pixelsPtr, 160 * sizeof(byte) * 3);

            SDL.SDL_RenderClear(renderer);
            SDL.SDL_RenderCopy(renderer, texture, IntPtr.Zero, IntPtr.Zero);
            SDL.SDL_RenderPresent(renderer);

            SDL.SDL_RenderClear(renderer);

            handle.Free();
        }
    }
}

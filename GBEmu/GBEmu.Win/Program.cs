using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GBEmu.Win
{
    public static class Settings
    {
        public static string FontBitmapFilename = "test.png";
        public static int GlyphsPerLine = 16;
        public static int GlyphLineCount = 16;
        public static int GlyphWidth = 11;
        public static int GlyphHeight = 22;

        public static int CharXSpacing = 11;

        public static string Text = "GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);";

        // Used to offset rendering glyphs to bitmap
        public static int AtlasOffsetX = -3, AtlassOffsetY = -1;
        public static int FontSize = 14;
        public static bool BitmapFont = false;
        public static string FromFile; //= "joystix monospace.ttf";
        public static string FontName = "Consolas";

    }

    public static class Program
    {
        private static void Main()
        {
            GenerateFontImage();

            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(800, 600),
                Title = "LearnOpenTK - Creating a Window",
            };

            // To create a new window, create a class that extends GameWindow, then call Run() on it.
            using (var window = new Window(GameWindowSettings.Default, nativeWindowSettings))
            {
                window.Run();
            }
        }

        static void GenerateFontImage()
        {
            int bitmapWidth = Settings.GlyphsPerLine * Settings.GlyphWidth;
            int bitmapHeight = Settings.GlyphLineCount * Settings.GlyphHeight;

            using (Bitmap bitmap = new Bitmap(bitmapWidth, bitmapHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                Font font;
                if (!String.IsNullOrWhiteSpace(Settings.FromFile))
                {
                    var collection = new PrivateFontCollection();
                    collection.AddFontFile(Settings.FromFile);
                    var fontFamily = new FontFamily(Path.GetFileNameWithoutExtension(Settings.FromFile), collection);
                    font = new Font(fontFamily, Settings.FontSize);
                }
                else
                {
                    font = new Font(new FontFamily(Settings.FontName), Settings.FontSize);
                }

                using (var g = Graphics.FromImage(bitmap))
                {
                    if (Settings.BitmapFont)
                    {
                        g.SmoothingMode = SmoothingMode.None;
                        g.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
                    }
                    else
                    {
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                        //g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    }

                    for (int p = 0; p < Settings.GlyphLineCount; p++)
                    {
                        for (int n = 0; n < Settings.GlyphsPerLine; n++)
                        {
                            char c = (char)(n + p * Settings.GlyphsPerLine);
                            g.DrawString(c.ToString(), font, Brushes.White,
                                n * Settings.GlyphWidth + Settings.AtlasOffsetX, p * Settings.GlyphHeight + Settings.AtlassOffsetY);
                        }
                    }
                }
                bitmap.Save(Settings.FontBitmapFilename);
            }
        }
    }
}

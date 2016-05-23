/* *
* Canas Uvighi, a RogueLike Game / RPG project.
* Copyright (C) 2015 Aleksandar Dimitrov (screen name SCiENiDE)
* 
* This program is free software: you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation, either version 3 of the License, or
* (at your option) any later version.
* 
* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.
* 
* You should have received a copy of the GNU General Public License
* along with this program.  If not, see <http://www.gnu.org/licenses/>.
* */

namespace ProjectRLG.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using ProjectRLG.Contracts;
    using ProjectRLG.Extensions;
    using System.Diagnostics;

    /*
    * * * * * * *
    * Guidelines for using colors in the Message Log.
    * * * * *
    * To draw a string/part of a string in a specific color use:
    ~[select_char][color_UInt]! - formatting sequence in string passed to FormatWriteOnPosition.
    --> '~' - denotes the beggining of a format sequence.
    --> [select_char] - selects the size/length of text to colour (case-insensitive). ->
        L - selects the next letter from the string.
        W - selects the next word (selection breaks on whitespace).
        S - selects the rest of the string.
    --> [color_UInt] - selects the color in which the string will be shown. ->
        Use Color.ToUInt() to get the UInt representation of a XNA Color.
        To parse the UInt to color use UInt.ToColor() (both methods are located inside 
        Framework.ColorExtensions).
    --> '!' - denotes end of sequence.
    */
    public sealed class MessageLog : IMessageLog
    {
        private const string GREETING = "Welcome-to-\"Project RogueLikeGame\"=v5.SCiENiDE-2016!";
        private const int TEXT_LEFT_PAD = 5;

        private readonly int SpaceScreenWidth;

        private Color _foregroundColor;
        private StringBuilder[] _lines;
        private SpriteFont _spriteFont;
        private Rectangle _zone;
        private Vector2[] _lineVectors;

        public MessageLog(Rectangle logZone, SpriteFont spriteFont)
        {
            _zone = logZone;
            _spriteFont = spriteFont;

            SpaceScreenWidth = TextScreenLength(" ");

            // Default text color
            _foregroundColor = Color.Gray;

            int lineCount = _zone.Height / FontHeight;
            _lines = new StringBuilder[lineCount];
            _lineVectors = new Vector2[lineCount];

            // Initialize line coordinates, from bottom [0], to top [Length - 1].
            for (int i = 0; i < lineCount; i++)
            {
                _lines[i] = new StringBuilder();

                _lineVectors[i] = new Vector2(
                    _zone.Left + TEXT_LEFT_PAD,
                    _zone.Bottom - (i * FontHeight));
            }

            ShowGreeting(GREETING);
        }

        public Color ForegroundColor
        {
            get { return _foregroundColor; }
            set { _foregroundColor = value; }
        }
        private int FontHeight
        {
            get { return _spriteFont.LineSpacing; }
        }
        public bool SendMessage(string text)
        {
            // Check if the text sent fits in the message rectangle.
            if (this.TextScreenLength(text) <= this._zone.Width)
            {
                for (int i = this._lines.Length - 1; i > 0; i--)
                {
                    this._lines[i].Clear();
                    this._lines[i].Append(this._lines[i - 1]);
                }

                this._lines[0].Clear();
                this._lines[0].Append(text);

                return true;
            }
            else
            {
                // The text line is too long, split in several lines.
                string[] splitText = text.Split(' ');
                int nextUnappanededString = 0;
                StringBuilder textFirstPart = new StringBuilder();

                for (int i = nextUnappanededString; i < splitText.Length; i++)
                {
                    int textLength = TEXT_LEFT_PAD + this.TextScreenLength(textFirstPart) + this.TextScreenLength(splitText[i]);

                    if (textLength + this.SpaceScreenWidth < this._zone.Width)
                    {
                        textFirstPart.Append(splitText[i]);
                        textFirstPart.Append(" ");
                    }
                    else
                    {
                        nextUnappanededString = i;
                        break;
                    }
                }

                StringBuilder textSecondPart = new StringBuilder();
                for (int i = nextUnappanededString; i < splitText.Length; i++)
                {
                    textSecondPart.Append(splitText[i]);
                    textSecondPart.Append(" ");
                }

                // Recursively send splitted messages.
                this.SendMessage(textFirstPart.ToString());
                this.SendMessage(textSecondPart.ToString());
            }

            return false;
        }
        public void ClearLog()
        {
            for (int i = 0; i < this._lines.Length; i++)
            {
                this._lines[i].Clear();
            }
        }
        public void DrawLog(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            for (int i = 0; i < this._lines.Length; i++)
            {
                #region Colored string draw

                int linePosition = 0;
                string workText = this._lines[i].ToString();
                char selector = '\0';
                StringBuilder color = new StringBuilder(10);

                for (int k = 0; k < workText.Length; k++)
                {
                    // Beggining of an escape formatting sequence.
                    if (workText[k] == '~')
                    {
                        // Get the selector character.
                        selector = workText[++k];

                        // Get the color code.
                        while (workText[++k] != '!')
                        {
                            color.Append(workText[k]);
                        }

                        k++;

                        // Select text to color.
                        string selectedText = string.Empty;

                        #region Text Select

                        switch (selector)
                        {
                            case 'L':
                            case 'l':
                                {
                                    selectedText = workText[k].ToString();
                                    break;
                                }

                            case 'W':
                            case 'w':
                                {
                                    StringBuilder sb = new StringBuilder(20);

                                    for (int j = k; j < workText.Length; j++)
                                    {
                                        bool endOfWord = char.IsWhiteSpace(workText[j]);

                                        if (!endOfWord)
                                        {
                                            sb.Append(workText[j]);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }

                                    selectedText = sb.ToString();
                                    break;
                                }

                            case 'S':
                            case 's':
                                {
                                    selectedText = new string(workText.ToCharArray(), k, workText.Length - k);
                                    selectedText = this.RemoveColorSeq(selectedText, true);
                                    break;
                                }

                            default:
                                {
                                    throw new ArgumentException(
                                        "Invalid selector char in MessageLog color string sequence!",
                                        "selector");
                                }
                        }

                        k--;

                        #endregion

                        uint colorUInt = uint.Parse(color.ToString());
                        Color parsedColor = colorUInt.ToColor();
                        color.Clear();

                        Vector2 newPosition = new Vector2(
                            this._lineVectors[i].X + linePosition,
                            this._lineVectors[i].Y);

                        // Set the line position for the next string.
                        linePosition += this.TextScreenLength(this.RemoveColorSeq(selectedText, true));

                        spriteBatch.DrawString(
                            this._spriteFont,
                            this.RemoveColorSeq(selectedText),
                            newPosition,
                            parsedColor);

                        k += this.RemoveColorSeq(selectedText, true).Length;
                    }
                    else
                    {
                        Vector2 newPosition = new Vector2(
                            this._lineVectors[i].X + linePosition,
                            this._lineVectors[i].Y);

                        spriteBatch.DrawString(
                            this._spriteFont,
                            this._lines[i][k].ToString(),
                            newPosition,
                            this._foregroundColor);

                        linePosition += this.TextScreenLength(this._lines[i][k].ToString());
                    }
                }
                #endregion
            }

            spriteBatch.End();
        }

        private int TextScreenLength(string text)
        {
            string clearedText = RemoveColorSeq(text);
            return (int)_spriteFont.MeasureString(clearedText).X;
        }
        private int TextScreenLength(StringBuilder text)
        {
            return TextScreenLength(text.ToString());
        }
        private string RemoveColorSeq(string text, bool breakOnSequence = false)
        {
            int idx = text.IndexOf('~');
            if (idx == -1)
            {
                return text;
            }

            StringBuilder actualText = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '~')
                {
                    if (breakOnSequence)
                    {
                        return actualText.ToString();
                    }

                    i += 13;
                }

                actualText.Append(text[i]);
            }

            return actualText.ToString();
        }
        private void ShowGreeting(string greeting)
        {
            List<Color> gradient = new List<Color>();
            float step = (float)1 / greeting.Length;
            for (double i = 0; i < 1; i += step)
            {
                gradient.Add(ColorUtilities.HSL2RGB(i, 0.5, 0.5));
            }

            StringBuilder gradientGreeting = new StringBuilder();
            for (int i = 0; i < greeting.Length; i++)
            {
                gradientGreeting.AppendFormat("~L{0}!{1}",
                    gradient[(gradient.Count + i) % gradient.Count].ToUInt(),
                    greeting[i]);
            }

            SendMessage(gradientGreeting.ToString());
        }
    }
}
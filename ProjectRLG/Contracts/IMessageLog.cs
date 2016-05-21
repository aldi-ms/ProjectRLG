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
/* Edited 21.05.2015 by Aleksandar Dimitrov for ProjectRLG. */

namespace ProjectRLG.Contracts
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public interface IMessageLog
    {
        /// <summary>
        /// Get or set the foreground color of the text.
        /// </summary>
        Color TextColor { get; set; }

        /// <summary>
        /// Send a message to the log buffer.
        /// </summary>
        /// <param name="text">String text message.</param>
        /// <returns>Indicates whether the message was successfuly shown.</returns>
        bool SendMessage(string text);

        /// <summary>
        /// Draw the log on the screen.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch used to draw the log.</param>
        void DrawLog(SpriteBatch spriteBatch);

        /// <summary>
        /// Clear the log.
        /// </summary>
        void ClearLog();
    }
}

﻿/*
Technitium DNS Server
Copyright (C) 2019  Shreyas Zare (shreyas@technitium.com)

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.

*/

using System.IO;
using System.Text;
using TechnitiumLibrary.IO;

namespace DnsServerCore.Dhcp.Options
{
    class MessageOption : DhcpOption
    {
        #region variables

        string _text;

        #endregion

        #region constructor

        public MessageOption(string text)
            : base(DhcpOptionCode.Message)
        {
            _text = text;
        }

        public MessageOption(Stream s)
            : base(DhcpOptionCode.Message, s)
        {
            int len = s.ReadByte();
            if (len < 0)
                throw new EndOfStreamException();

        }

        #endregion

        #region protected

        protected override void ParseOptionValue(Stream s)
        {
            if (s.Length < 1)
                throw new InvalidDataException();

            _text = Encoding.ASCII.GetString(s.ReadBytes((int)s.Length));
        }

        protected override void WriteOptionValue(Stream s)
        {
            s.Write(Encoding.ASCII.GetBytes(_text));
        }

        #endregion

        #region properties

        public string Text
        { get { return _text; } }

        #endregion
    }
}
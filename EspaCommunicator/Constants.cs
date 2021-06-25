using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EspaCommunicator
{
    public static class Constants
    {

        // start of the header
        public static char SOH = (char)0x1;

        // start of text
        public static char STX = (char)0x2;

        // end of the text 
        public static char ETX = (char)0x3;

        // end of transmission  
        public static char EOT = (char)0x4;	

        // enquire (request)
        public static char ENQ = (char)0x5;	

        // acknowledge (postitive confirmation)
        public static char ACK = (char)0x6;

        // not acknowledge (negativ confirmation)
        public static char NAK = (char)0x15;

        // record seperator 
        public static char RS = (char)0x1E;

        // unit seperator
        public static char US = (char)0x1F;

        public static readonly string[] AsciiTable = new string[] {
            "NUL",
            "SOH",
            "STX",
            "ETX",
            "EOT",
            "ENQ",
            "ACK",
            "BEL",
            "BS",
            "HT",
            "LF",
            "VT",
            "FF",
            "CR",
            "SO",
            "SI",
            "DLE",
            "DC1",
            "DC2",
            "DC3",
            "DC4",
            "NAK",
            "SYN",
            "ETB",
            "CAN",
            "EM",
            "SUB",
            "ESC",
            "FS",
            "GS",
            "RS",
            "US",
            "SPC",
            "!",
            "\"",
            "#",
            "$",
            "%",
            "&",
            "'",
            "(",
            ")",
            "*",
            "+",
            ",",
            "-",
            ".",
            "/",
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            ":",
            ";",
            "<",
            "=",
            ">",
            "?",
            "@",
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z",
            "[",
            "\\",
            "]",
            "^",
            "_",
            "`",
            "a",
            "b",
            "c",
            "d",
            "e",
            "f",
            "g",
            "h",
            "i",
            "j",
            "k",
            "l",
            "m",
            "n",
            "o",
            "p",
            "q",
            "r",
            "s",
            "t",
            "u",
            "v",
            "w",
            "x",
            "y",
            "z",
            "{",
            "|",
            "}",
            "~",
            "DEL"
        };


        // header types
        public static char ESPA_H_CALLTOPAGER	= '1';
        public static char ESPA_H_STAT_INFO	= '2';
        public static char ESPA_H_STAT_REQUEST = '3';
        public static char ESPA_H_CALLTOSUBSC_LINE = '4';
        public static char ESPA_H_OTHER_INFO = '5';

        public static char ESPA_RECT_CALLADDRESS = '1';		// pager-(group-) address/telephonenumber, max 16 signs
        public static char ESPA_RECT_DISPMESSAGE = '2';		// message, max 128 signs			
        public static char ESPA_RECT_BEEPCODING = '3';		// '0' = reserved; '1'-'9'= system dependend
        public static char ESPA_RECT_CALLTYPE = '4';		// '1' = reset-(cancel); '2' = speech; '3' = standard
        public static char ESPA_RECT_NO_OF_TRANSMIS = '5';		// number of calls; '0' reserved; '1', '2' etc.
        public static char ESPA_RECT_PRIORITY = '6';		// priority: '1' = alarm; '2' = high; '3' = high
        public static char ESPA_RECT_CALL_STATUS = '7';		// '1' = busy; '2' = in queue; '3' = paged... aso.				(NOT IN USE)
        public static char ESPA_RECT_SYSTEM_STATUS = '8';		// '0' = reserved; '1' = transmitter failure					(NOT IN USE)

        public static char ESPA_STANDARD_ADDRESS_CTRL_STN = '1';

        public static int MAX_STRUCTURE_SIZE = 128;
        public static int ESPA_MAX_RECORDS_PER_STRUCTURE = 6;
        public static int ESPA_MAX_DATASIZE_MESSAGE = 128;
        public static int ESPA_MAX_DATASIZE_CALLADDR = 16;
        public static int ESPA_MAX_DATASIZE_BEEPCODING = 1;

        public static int ESPA_END_OF_TRANSMISSION_LENGTH = 1;
        public static int ESPA_SELECT_SEQ_REQ_LENGTH = 4;

    }
}

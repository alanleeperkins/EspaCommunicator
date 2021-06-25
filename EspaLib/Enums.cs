using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EspaLib
{
    public enum eReturnValue {SerialComError = -4, EspaRecordReadError = -3, LicenseError = -2, GeneralError = -1, Ok=0 };
    public enum eTrafficDirection { Send = 0, Receive = 1 };
    public enum eEspaDataBlockType { Records, SelectSequenze, PollSequenze };
    public enum eEspaDeviceType {None=-1, Station=0, ControlStation=1 };
    public enum eEspaDeviceState { Idle = 0, TemporaryMasterStation =1, SlaveStation =2 };
    public enum eEspaRequestType { None = 0, Poll = 1, Select = 2, RecordDataBlock = 3, ControlSign = 4 };

    public enum eEspaStandards
    {
        AddressControlStation = (byte) '1',
        StandardHeaderIdentifier = (byte) '2',
        StandardMasterAddress = (byte)'1',
        StandardSlaveAddress = (byte)'2',
        LowestAddress = (byte)'1',
        HighestAddress = (byte)'9'
    }

    public enum eEspaConstants
    {
        MaxSizeStructure = (int)1024,
        MaxRecordsPerStructure = (int)6,
        MaxSizeMessage = (int)128,
        MaxSizeCallAddress = (int)16,
        MaxSizeBeepCoding = (int)1,
        MaxRecordTypes = (int)8,
        LengthSelectSequenceRequest = (int)4,
        SizeMasterResponseFiFo = (int)4000,
        SimulateControlStationInterval = (int)250,
        SimulateStationInterval = (int)250,
        DefaultPollingInterval = (int)5,
        MaxPollingInterval = (int)30,
        MinPollingInteval = (int)1,

        /// <summary>
        /// if the control station does not detect any valid transaction on the communiction line,
        /// within <Timout> milliseconds, then it sends <EOT> to terminate and regain control
        /// standard: 10 sec
        /// </summary>
        ControlStationRegainControlTimout = 10,
    };

    /// <summary>
    /// ESPA Protocol 4.4.4 Header Types
    /// </summary>
    public enum eEspaHeaderTypes
    {
        CallToPager = (byte)'1',
        StatusInformation = (byte)'2',
        StatusRequest= (byte)'3',
        CallToSubscriberLine = (byte)'4',
        OtherInformation = (byte)'5'
    };

    /// <summary>
    /// ESPA Protocol 4.4.4 Record Types
    /// </summary>
    public enum eEspaRecordTypes
    {
        /// <summary>
        /// pager-(group-) address/telephonenumber, max 16 signs
        /// </summary>
        CallAddress  = (byte)'1',

        /// <summary>
        /// message, max 128 signs
        /// </summary>
        DisplayMessage  = (byte)'2',

        /// <summary>
        /// '0' = reserved; '1'-'9'= system dependend
        /// </summary>
        BeepCoding = (byte)'3',

        /// <summary>
        /// '1' = reset-(cancel); '2' = speech; '3' = standard
        /// </summary>
        CallType = (byte)'4',

        /// <summary>
        /// number of calls; '0' reserved; '1', '2' etc.
        /// </summary>
        NumberOfTransmissions = (byte)'5',

        /// <summary>
        /// priority: '1' = alarm; '2' = high; '3' = high
        /// </summary>
        Priority = (byte)'6',

        /// <summary>
        /// '1' = busy; '2' = in queue; '3' = paged... aso.
        /// </summary>
        CallStatus = (byte)'7',

        /// <summary>
        /// '0' = reserved; '1' = transmitter failure
        /// </summary>
        SystemStatus = (byte)'8',
    };


    /// <summary>
    /// TRANSMISSION CONTROL PREFIXES 
    /// </summary>
    public enum eTraCtrlPrefix
    {
        /// <summary>
        /// Corrupt character(s) or corrupt BCC, received by the station (BCC Block Checking Character, ISO 1155) 
        /// </summary>
        TransmissionError = (byte)'1',

        /// <summary>
        /// Unable to accept a transaction e.g. queue full etc 
        /// </summary>
        Busy = (byte)'2',

        /// <summary>
        /// Type or content of message not recognised by this station  
        /// </summary>
        InvalidMessage  = (byte)'3'
    };

    /// <summary>
    ///  ASCII Control characters
    /// </summary>
    public enum eAsciiCtrl 
    {
        /// <summary>
        /// start of header
        /// </summary>
        SOH = (byte)0x1,

        /// <summary>
        /// start of text
        /// </summary>
        STX = (byte)0x2,

        /// <summary>
        /// end of the text
        /// </summary>
        ETX = (byte)0x3,

        /// <summary>
        /// end of transmission
        /// </summary>
        EOT = (byte)0x4,

        /// <summary>
        /// enquire (request)
        /// </summary>
        ENQ = (byte)0x5,

        /// <summary>
        /// acknowledge (postitive confirmation)
        /// </summary>
        ACK = (byte)0x6,

        /// <summary>
        /// not acknowledge (negativ confirmation)
        /// </summary>
        NAK = (byte)0x15,

        /// <summary>
        /// record seperator
        /// </summary>
        RS = (byte)0x1E,

        /// <summary>
        /// unit seperator
        /// </summary>
        US = (byte)0x1F
    };

}

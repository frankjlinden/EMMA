using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;


namespace CT.DDS.EMMA.Models
{
    public enum SyncExecStatus
    {
        [Description("Success")]
        Success = 0,

        [Description("The source connection or query failed.")]
        SourceQueryFail = 1,

        [Description("The source query returned no results.")]
        SourceNoResult = 2,

        [Description("Failed to save to target db.")]
        TargetDbFail = 3,

        [Description("Program encounterd an error")]
        ProgramError = 5,

        [Description("Execution failed")]
        ExecuteError = 6,

    }

    public enum SendExecStatus
    {
        [Description("Success")]
        Success = 0,

        [Description("Completed with send errors.")]
        CompletedWithErrors = 1,


        [Description("SMTP Connection failure")]
        ConnectFail = 2,

        [Description("SMTP Authentication failure")]
        AuthFail = 3,

        [Description("Program exception")]
        ProgramError = 5,

        [Description("Execution error")]
        ExecuteError = 6,

    }

    public enum MessageStatus
    {

        [Description("Sent")]
        Sent = 0,

        [Description("Recipient not accepted")]
        RecipientNotAccepted = 1,

        [Description("Sender not accepted")]
        SenderNotAccepted = 2,

        [Description("Message not accepted")]
        MessageNotAccepted = 3,

        [Description("Command error")]
        CommandError = 4,

        [Description("Protocol error")]
        ProtocolError = 5,

        [Description("Program error")]
        ProgramError = 6,

        [Description("New unsent message")]
        NewUnsent = 99

    }


}

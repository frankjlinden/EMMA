using CT.DDS.EMMA.Sync.Data;
using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace CT.DDS.EMMA.Models
{
 
        public static class Extensions
        {
            //ENUM extension... get description
            public static string GetDescription(this Enum value)
            {
                FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
                if (fieldInfo == null) return null;
                var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
                return attribute.Description;
            }

            public static string ResolveTemplate(this string template, Dictionary<string, string> valDict)
            {
                foreach (KeyValuePair<string, string> pair in valDict)
                {
                    template = template.Replace("{" + pair.Key + "}", pair.Value);
                }

                return template;

            }
            /// <summary>
            /// Convert simple Message model into Mailkit MimeMessage object
            /// </summary>
            /// <param name="msg"></param>
            /// <returns></returns>


            public static MimeMessage ConvertToMimeMessage(this Message msg)
            {
                string[] addresses;

                MimeMessage mMessage = new MimeMessage();

                mMessage.From.Add(new MailboxAddress(msg.From));
                // message.Status
                // To address may be an address list.
                // Split by semicolon and add an addresss for each one to Bcc

                //To
                if (!string.IsNullOrEmpty(msg.To))
                {
                    addresses = msg.To.Split(";", StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < addresses.Length; i++)
                        mMessage.Bcc.Add(new MailboxAddress(addresses[i]));
                }
                else
                {
                    msg.Status = MessageStatus.RecipientNotAccepted;
                    msg.ErrorText = "No Recipient specified";
                }
                //Cc
                if (!string.IsNullOrEmpty(msg.Cc))
                {
                    addresses = msg.Cc.Split(";", StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < addresses.Length; i++)
                        mMessage.Bcc.Add(new MailboxAddress(addresses[i]));
                }

                // Bcc
                //Cc
                if (!string.IsNullOrEmpty(msg.Bcc))
                {
                    addresses = msg.Bcc.Split(";", StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < addresses.Length; i++)
                        mMessage.Bcc.Add(new MailboxAddress(addresses[i]));
                }

                if (!string.IsNullOrEmpty(msg.From))
                {
                    mMessage.Sender = new MailboxAddress(msg.From);
                }

                if (msg.Subject != null)
                    mMessage.Subject = msg.Subject;

                if (!string.IsNullOrEmpty(msg.Body))
                {
                    mMessage.Body = new TextPart("plain") { Text = msg.Body };
                }
                else
                {
                    msg.Status = MessageStatus.MessageNotAccepted;
                    msg.ErrorText = "Email Body not present.";
                }

                return mMessage;
            }


            public static Message ConvertToMessage(this DbRow row)
            {
                Message message = new Message();

                // When converting from dbRow to simple message, resolve the body and subject because the required info is only present here.
                //    but don't split the addresses until creating the actual MimeMessage later
                // 
                if (row.ValuesDictionary.ContainsKey("To"))
                {
                    message.To = row.ValuesDictionary["To"];
                }
                if (row.ValuesDictionary.ContainsKey("Cc"))
                {
                    message.To = row.ValuesDictionary["Cc"];
                }
                if (row.ValuesDictionary.ContainsKey("Bcc"))
                {
                    message.Bcc = row.ValuesDictionary["Bcc"];
                }

                return message;
            }


        }
    }


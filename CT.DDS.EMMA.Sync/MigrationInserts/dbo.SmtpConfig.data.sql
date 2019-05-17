use emma
go
SET IDENTITY_INSERT [dbo].[SmtpConfig] ON
INSERT INTO [dbo].[SmtpConfig] ([Id], [ConfigName],[Host], [UserName], [Password], [Port], [UseAuthentication], [SecureSocketOptions]) VALUES (1, N'Exchange',N'po.state.ct.us', NULL, NULL, 25, 0, 0)
INSERT INTO [dbo].[SmtpConfig] ([Id], [ConfigName],[Host], [UserName], [Password], [Port], [UseAuthentication], [SecureSocketOptions]) VALUES (2, N'Gmail',N'smtp.gmail.com', N'frankjlindenct@gmail.com', N'P@ss4CTGaccount', 587, 1, 2)
SET IDENTITY_INSERT [dbo].[SmtpConfig] OFF

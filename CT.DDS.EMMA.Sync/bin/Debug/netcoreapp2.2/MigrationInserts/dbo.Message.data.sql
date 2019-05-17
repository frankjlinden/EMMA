use emma
go
DELETE FROM Message;
SET IDENTITY_INSERT [dbo].[Message] ON
INSERT INTO [dbo].[Message] ([Id], [SysStart], [SysEnd], [SysUser], [SysUserNext], [To], [From], [Cc], [Bcc], [Subject], [Body], [AttemptCount], [JobConfigId],  [ErrorText], [Status], [ExecutionId]) VALUES (1, N'2018-01-01 00:00:00', N'9999-12-30 00:00:00', N'Frank.Linden@ct.gov', NULL, N'Frank.Linden@ct.gov', N'Frank.Linden@ct.gov', NULL, NULL, N'Test', N'test', 1, 1,  NULL, 5, 1);
INSERT INTO [dbo].[Message] ([Id], [SysStart], [SysEnd], [SysUser], [SysUserNext], [To], [From], [Cc], [Bcc], [Subject], [Body], [AttemptCount], [JobConfigId],  [ErrorText], [Status], [ExecutionId]) VALUES (2, N'2018-01-01 00:00:00', N'9999-12-30 00:00:00', N'Frank.Linden@ct.gov', NULL, N'frankjlinden@yahoo.com', N'frankjlindenct@gmail.com', NULL, NULL, N'Test', N'test', 1, 1, NULL, 5,1);

SET IDENTITY_INSERT [dbo].[Message] OFF
USE [LibraryManagement]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 09/10/2024 14:07:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AvailableBookNotification]    Script Date: 09/10/2024 14:07:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AvailableBookNotification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[NotificationDate] [datetime] NULL,
	[IsNotificationSent] [bit] NOT NULL,
 CONSTRAINT [availablebooknotification_pkey] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Book]    Script Date: 09/10/2024 14:07:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](255) NOT NULL,
	[Author] [varchar](255) NOT NULL,
	[Description] [text] NOT NULL,
	[IsAvailable] [bit] NOT NULL,
 CONSTRAINT [book_pkey] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookReservation]    Script Date: 09/10/2024 14:07:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookReservation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[StartDate] [datetime] NULL,
	[ReturnDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[ReservedDate] [datetime] NOT NULL,
	[ReservationStatus] [bit] NOT NULL,
	[RequestAvailabilityNotification] [bit] NOT NULL,
 CONSTRAINT [bookreservation_pkey] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 09/10/2024 14:07:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](255) NOT NULL,
	[LastName] [varchar](255) NOT NULL,
	[Address] [varchar](255) NOT NULL,
	[PhoneNumber] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
 CONSTRAINT [customer_pkey] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OAuthAuthentication]    Script Date: 09/10/2024 14:07:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OAuthAuthentication](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[ClientId] [varchar](255) NOT NULL,
	[ClientSecret] [varchar](255) NOT NULL,
	[Scope] [varchar](255) NOT NULL,
	[GrantType] [varchar](255) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [oauthauthentication_pkey] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241008161253_LibraryManagementMigrate', N'8.0.8')
GO
SET IDENTITY_INSERT [dbo].[AvailableBookNotification] ON 

INSERT [dbo].[AvailableBookNotification] ([Id], [BookId], [CustomerId], [NotificationDate], [IsNotificationSent]) VALUES (1, 1, 2, CAST(N'2024-10-09T08:14:33.260' AS DateTime), 1)
INSERT [dbo].[AvailableBookNotification] ([Id], [BookId], [CustomerId], [NotificationDate], [IsNotificationSent]) VALUES (2, 2, 3, CAST(N'2024-10-09T09:29:55.743' AS DateTime), 1)
INSERT [dbo].[AvailableBookNotification] ([Id], [BookId], [CustomerId], [NotificationDate], [IsNotificationSent]) VALUES (3, 1, 3, CAST(N'2024-10-09T11:20:54.420' AS DateTime), 1)
INSERT [dbo].[AvailableBookNotification] ([Id], [BookId], [CustomerId], [NotificationDate], [IsNotificationSent]) VALUES (4, 2, 2, CAST(N'2024-10-09T12:12:31.763' AS DateTime), 1)
INSERT [dbo].[AvailableBookNotification] ([Id], [BookId], [CustomerId], [NotificationDate], [IsNotificationSent]) VALUES (5, 3, 2, CAST(N'2024-10-09T12:40:35.390' AS DateTime), 1)
INSERT [dbo].[AvailableBookNotification] ([Id], [BookId], [CustomerId], [NotificationDate], [IsNotificationSent]) VALUES (6, 2, 4, CAST(N'2024-10-09T12:40:35.507' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[AvailableBookNotification] OFF
GO
SET IDENTITY_INSERT [dbo].[Book] ON 

INSERT [dbo].[Book] ([Id], [Title], [Author], [Description], [IsAvailable]) VALUES (1, N'Origin Africa', N'Jonathan Kingdon', N'Origin Africa is a unique introduction to the natural history and evolution of the most misrepresented continent on Earth. Celebrated evolutionary biologist and artist', 1)
INSERT [dbo].[Book] ([Id], [Title], [Author], [Description], [IsAvailable]) VALUES (2, N'Jungle Book', N'Rudyard Kipling', N'This book is a science fiction that depicts the world animals live in the forest and the intereaction they have with a boy', 1)
INSERT [dbo].[Book] ([Id], [Title], [Author], [Description], [IsAvailable]) VALUES (3, N'Gulliver''s Travels', N'Jonathan Swift', N'A science fiction book about a boy that travel to different part of the world and meeting people of large varying sizes', 1)
SET IDENTITY_INSERT [dbo].[Book] OFF
GO
SET IDENTITY_INSERT [dbo].[BookReservation] ON 

INSERT [dbo].[BookReservation] ([Id], [BookId], [CustomerId], [StartDate], [ReturnDate], [EndDate], [ReservedDate], [ReservationStatus], [RequestAvailabilityNotification]) VALUES (1, 1, 2, CAST(N'2024-10-09T07:30:05.000' AS DateTime), CAST(N'2024-10-09T08:14:33.260' AS DateTime), CAST(N'2024-10-10T09:30:05.000' AS DateTime), CAST(N'2024-10-09T05:33:29.343' AS DateTime), 0, 1)
INSERT [dbo].[BookReservation] ([Id], [BookId], [CustomerId], [StartDate], [ReturnDate], [EndDate], [ReservedDate], [ReservationStatus], [RequestAvailabilityNotification]) VALUES (2, 2, 3, CAST(N'2024-10-09T08:30:05.000' AS DateTime), CAST(N'2024-10-09T09:29:55.667' AS DateTime), CAST(N'2024-10-11T09:30:05.000' AS DateTime), CAST(N'2024-10-09T06:22:19.050' AS DateTime), 0, 1)
INSERT [dbo].[BookReservation] ([Id], [BookId], [CustomerId], [StartDate], [ReturnDate], [EndDate], [ReservedDate], [ReservationStatus], [RequestAvailabilityNotification]) VALUES (3, 3, 3, NULL, NULL, NULL, CAST(N'2024-10-08T06:58:13.673' AS DateTime), 0, 1)
INSERT [dbo].[BookReservation] ([Id], [BookId], [CustomerId], [StartDate], [ReturnDate], [EndDate], [ReservedDate], [ReservationStatus], [RequestAvailabilityNotification]) VALUES (4, 1, 3, NULL, NULL, NULL, CAST(N'2024-10-08T09:53:52.403' AS DateTime), 0, 1)
INSERT [dbo].[BookReservation] ([Id], [BookId], [CustomerId], [StartDate], [ReturnDate], [EndDate], [ReservedDate], [ReservationStatus], [RequestAvailabilityNotification]) VALUES (5, 2, 3, NULL, NULL, NULL, CAST(N'2024-10-08T10:42:49.583' AS DateTime), 0, 1)
INSERT [dbo].[BookReservation] ([Id], [BookId], [CustomerId], [StartDate], [ReturnDate], [EndDate], [ReservedDate], [ReservationStatus], [RequestAvailabilityNotification]) VALUES (6, 1, 2, NULL, NULL, NULL, CAST(N'2024-10-09T11:39:51.273' AS DateTime), 0, 1)
INSERT [dbo].[BookReservation] ([Id], [BookId], [CustomerId], [StartDate], [ReturnDate], [EndDate], [ReservedDate], [ReservationStatus], [RequestAvailabilityNotification]) VALUES (7, 3, 4, CAST(N'2024-10-09T08:30:05.000' AS DateTime), CAST(N'2024-10-09T13:02:00.333' AS DateTime), CAST(N'2024-10-09T09:29:55.667' AS DateTime), CAST(N'2024-10-08T12:17:08.367' AS DateTime), 0, 0)
INSERT [dbo].[BookReservation] ([Id], [BookId], [CustomerId], [StartDate], [ReturnDate], [EndDate], [ReservedDate], [ReservationStatus], [RequestAvailabilityNotification]) VALUES (8, 2, 2, NULL, NULL, NULL, CAST(N'2024-10-08T12:23:19.567' AS DateTime), 0, 1)
SET IDENTITY_INSERT [dbo].[BookReservation] OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Address], [PhoneNumber], [Email]) VALUES (2, N'Jide', N'Ayoba', N'no. 12 yaba road', N'080238282279', N'jide@yahoo.com')
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Address], [PhoneNumber], [Email]) VALUES (3, N'Johnson', N'Umbrako', N'Umbrako Edited', N'09023929382', N'johnson@yahoo.com')
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Address], [PhoneNumber], [Email]) VALUES (4, N'Francis', N'Williams', N'no. 4 Jamiu Street, Ogba, Ikeja', N'070239238272', N'francis@yahoo.com')
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
ALTER TABLE [dbo].[AvailableBookNotification]  WITH CHECK ADD  CONSTRAINT [fk_book_availablebooknotifications] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AvailableBookNotification] CHECK CONSTRAINT [fk_book_availablebooknotifications]
GO
ALTER TABLE [dbo].[AvailableBookNotification]  WITH CHECK ADD  CONSTRAINT [fk_customer_availablebooknotifications] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AvailableBookNotification] CHECK CONSTRAINT [fk_customer_availablebooknotifications]
GO
ALTER TABLE [dbo].[BookReservation]  WITH CHECK ADD  CONSTRAINT [fk_book_bookreservations] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookReservation] CHECK CONSTRAINT [fk_book_bookreservations]
GO
ALTER TABLE [dbo].[BookReservation]  WITH CHECK ADD  CONSTRAINT [fk_customer_bookreservations] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookReservation] CHECK CONSTRAINT [fk_customer_bookreservations]
GO

USE [master]
GO
/****** Object:  Database [Bibloteka]    Script Date: 2/20/2025 7:58:05 PM ******/
CREATE DATABASE [Bibloteka]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Bibloteka', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Bibloteka.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Bibloteka_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Bibloteka_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Bibloteka] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Bibloteka].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Bibloteka] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Bibloteka] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Bibloteka] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Bibloteka] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Bibloteka] SET ARITHABORT OFF 
GO
ALTER DATABASE [Bibloteka] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Bibloteka] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Bibloteka] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Bibloteka] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Bibloteka] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Bibloteka] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Bibloteka] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Bibloteka] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Bibloteka] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Bibloteka] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Bibloteka] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Bibloteka] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Bibloteka] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Bibloteka] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Bibloteka] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Bibloteka] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Bibloteka] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Bibloteka] SET RECOVERY FULL 
GO
ALTER DATABASE [Bibloteka] SET  MULTI_USER 
GO
ALTER DATABASE [Bibloteka] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Bibloteka] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Bibloteka] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Bibloteka] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Bibloteka] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Bibloteka] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Bibloteka', N'ON'
GO
ALTER DATABASE [Bibloteka] SET QUERY_STORE = ON
GO
ALTER DATABASE [Bibloteka] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Bibloteka]
GO
/****** Object:  UserDefinedFunction [dbo].[CalculateDelayedLoanFees]    Script Date: 2/20/2025 7:58:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[CalculateDelayedLoanFees]()
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @TotalPenalty DECIMAL(10,2);

    -- Llogarit tarifën totale të vonesave për të gjitha huazimet që janë kthyer pas datës së kthimit
    SELECT @TotalPenalty = SUM(Penalty_Fee)
    FROM Loans
    WHERE Actual_Return_Date > Return_Date;

    RETURN @TotalPenalty;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[CalculatePenaltyFee]    Script Date: 2/20/2025 7:58:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[CalculatePenaltyFee]
(
    @Loan_ID INT
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @Return_Date DATE, @Actual_Return_Date DATE, @Penalty DECIMAL(10,2);

    -- Merr datat e kthimit dhe të kthimit real
    SELECT @Return_Date = Return_Date, @Actual_Return_Date = Actual_Return_Date
    FROM Loans WHERE Loan_ID = @Loan_ID;

    -- Llogarit penalitetin nëse huazimi është kthyer vonë
    IF @Actual_Return_Date > @Return_Date
    BEGIN
        SET @Penalty = DATEDIFF(DAY, @Return_Date, @Actual_Return_Date) * 0.50; -- 0.50€ për çdo ditë vonesë
    END
    ELSE
    BEGIN
        SET @Penalty = 0;
    END

    RETURN @Penalty;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[CheckIfClientHasLoanedMaterials]    Script Date: 2/20/2025 7:58:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[CheckIfClientHasLoanedMaterials]
(
    @Client_ID INT
)
RETURNS BIT
AS
BEGIN
    DECLARE @HasLoaned BIT;
    
    -- Kontrollon nëse klienti ka materiale të huazuara
    IF EXISTS (SELECT 1 FROM Loans WHERE Client_ID = @Client_ID)
    BEGIN
        SET @HasLoaned = 1; -- Klienti ka materiale të huazuara
    END
    ELSE
    BEGIN
        SET @HasLoaned = 0; -- Klienti nuk ka materiale të huazuara
    END

    RETURN @HasLoaned;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[GetAverageLoansPerClient]    Script Date: 2/20/2025 7:58:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetAverageLoansPerClient]
(
    @Client_ID INT
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @TotalLoans INT, @TotalClients INT;
    
    -- Merr numrin total të huazimeve për klientin
    SELECT @TotalLoans = COUNT(Loan_ID)
    FROM Loans
    WHERE Client_ID = @Client_ID;

    -- Merr numrin total të klientëve (për të llogaritur mesataren)
    SELECT @TotalClients = COUNT(DISTINCT Client_ID)
    FROM Loans;

    -- Llogarit mesataren e huazimeve për klient
    RETURN CAST(@TotalLoans AS DECIMAL(10,2)) / @TotalClients;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[GetClientPaymentBalance]    Script Date: 2/20/2025 7:58:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetClientPaymentBalance]
(
    @Client_ID INT
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @TotalPayments DECIMAL(10,2);

    -- Merr totalin e pagesave të bëra nga klienti
    SELECT @TotalPayments = SUM(Amount)
    FROM Payments
    WHERE Client_ID = @Client_ID;

    RETURN @TotalPayments;
END;
GO
/****** Object:  Table [dbo].[Bibliographic_Materials]    Script Date: 2/20/2025 7:58:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bibliographic_Materials](
	[Material_ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](255) NOT NULL,
	[Author] [varchar](255) NOT NULL,
	[Co_Authors] [varchar](255) NULL,
	[Publisher] [varchar](255) NULL,
	[Publication_Date] [date] NULL,
	[ISBN] [varchar](13) NULL,
	[DOI] [varchar](255) NULL,
	[Material_Type] [varchar](50) NULL,
	[Abstract] [text] NULL,
	[Available_Copies] [int] NULL,
	[Date_Added] [date] NULL,
	[Times_Loaned] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Material_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 2/20/2025 7:58:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[Client_ID] [int] IDENTITY(1,1) NOT NULL,
	[First_Name] [varchar](100) NOT NULL,
	[Last_Name] [varchar](100) NOT NULL,
	[Date_of_Birth] [date] NULL,
	[Email] [varchar](100) NOT NULL,
	[Phone] [varchar](20) NULL,
	[Address] [text] NULL,
	[Membership_Active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Client_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Loans]    Script Date: 2/20/2025 7:58:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Loans](
	[Loan_ID] [int] IDENTITY(1,1) NOT NULL,
	[Client_ID] [int] NOT NULL,
	[Material_ID] [int] NOT NULL,
	[Loan_Date] [date] NOT NULL,
	[Return_Date] [date] NOT NULL,
	[Actual_Return_Date] [date] NULL,
	[Penalty_Fee] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[Loan_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[DelayedLoans]    Script Date: 2/20/2025 7:58:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DelayedLoans] AS
SELECT 
    L.Loan_ID, 
    C.First_Name, 
    C.Last_Name, 
    B.Title, 
    L.Loan_Date, 
    L.Return_Date, 
    L.Actual_Return_Date, 
    L.Penalty_Fee
FROM Loans L
INNER JOIN Clients C ON L.Client_ID = C.Client_ID
INNER JOIN Bibliographic_Materials B ON L.Material_ID = B.Material_ID
WHERE L.Actual_Return_Date > L.Return_Date;
GO
/****** Object:  View [dbo].[ActiveClients]    Script Date: 2/20/2025 7:58:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ActiveClients] AS
SELECT 
    Client_ID, 
    First_Name, 
    Last_Name, 
    Date_of_Birth, 
    Email, 
    Phone, 
    Address
FROM Clients
WHERE Membership_Active = 1;
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 2/20/2025 7:58:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[Payment_ID] [int] IDENTITY(1,1) NOT NULL,
	[Client_ID] [int] NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[Payment_Date] [date] NULL,
	[Payment_Type] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Payment_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[TotalPaymentsPerClient]    Script Date: 2/20/2025 7:58:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TotalPaymentsPerClient] AS
SELECT 
    C.Client_ID, 
    C.First_Name, 
    C.Last_Name, 
    SUM(P.Amount) AS Total_Paid
FROM Payments P
INNER JOIN Clients C ON P.Client_ID = C.Client_ID
GROUP BY C.Client_ID, C.First_Name, C.Last_Name;
GO
/****** Object:  View [dbo].[ClientsWithPenalty]    Script Date: 2/20/2025 7:58:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ClientsWithPenalty] AS
SELECT 
    C.Client_ID, 
    C.First_Name, 
    C.Last_Name, 
    B.Title, 
    L.Loan_Date, 
    L.Return_Date, 
    L.Penalty_Fee
FROM Loans L
INNER JOIN Clients C ON L.Client_ID = C.Client_ID
INNER JOIN Bibliographic_Materials B ON L.Material_ID = B.Material_ID
WHERE L.Penalty_Fee > 0;
GO
/****** Object:  View [dbo].[MaterialsByType]    Script Date: 2/20/2025 7:58:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MaterialsByType] AS
SELECT 
    Material_Type, 
    COUNT(*) AS Total_Materials,
    SUM(Available_Copies) AS Total_Available
FROM Bibliographic_Materials
GROUP BY Material_Type;
GO
/****** Object:  UserDefinedFunction [dbo].[GetMostLoanedMaterials]    Script Date: 2/20/2025 7:58:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetMostLoanedMaterials]()
RETURNS TABLE
AS
RETURN
(
    SELECT 
        Material_ID,
        COUNT(Loan_ID) AS Loan_Count
    FROM Loans
    GROUP BY Material_ID
    HAVING COUNT(Loan_ID) > 1
);
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2/20/2025 7:58:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[User_ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[Role] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[User_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Bibliographic_Materials] ON 

INSERT [dbo].[Bibliographic_Materials] ([Material_ID], [Title], [Author], [Co_Authors], [Publisher], [Publication_Date], [ISBN], [DOI], [Material_Type], [Abstract], [Available_Copies], [Date_Added], [Times_Loaned]) VALUES (1, N'Introduction to Mechanical Engineering', N'John Doe', N'Jane Smith', N'Engineering Press', CAST(N'2022-03-01' AS Date), N'9781234567890', N'10.1000/intromech', N'Book', N'A comprehensive guide to the principles of mechanical engineering.', 5, CAST(N'2025-02-07' AS Date), NULL)
INSERT [dbo].[Bibliographic_Materials] ([Material_ID], [Title], [Author], [Co_Authors], [Publisher], [Publication_Date], [ISBN], [DOI], [Material_Type], [Abstract], [Available_Copies], [Date_Added], [Times_Loaned]) VALUES (2, N'Advanced Computer Algorithms', N'Alice Johnson', NULL, N'TechBooks', CAST(N'2021-11-15' AS Date), N'9780987654321', N'10.1000/advcomputers', N'Book', N'In-depth study of advanced algorithms used in computer science.', 3, CAST(N'2025-02-07' AS Date), NULL)
INSERT [dbo].[Bibliographic_Materials] ([Material_ID], [Title], [Author], [Co_Authors], [Publisher], [Publication_Date], [ISBN], [DOI], [Material_Type], [Abstract], [Available_Copies], [Date_Added], [Times_Loaned]) VALUES (3, N'Machine Learning  Basics', N'Mark Lee', N'Susan Brown', N'Data Science Press', CAST(N'2020-07-23' AS Date), N'9781122334455', N'10.1000/mlbasics', N'Article', N'Introduction to machine learning concepts and techniques.', 2, CAST(N'2025-02-07' AS Date), NULL)
INSERT [dbo].[Bibliographic_Materials] ([Material_ID], [Title], [Author], [Co_Authors], [Publisher], [Publication_Date], [ISBN], [DOI], [Material_Type], [Abstract], [Available_Copies], [Date_Added], [Times_Loaned]) VALUES (4, N'Understanding Artificial Intelligence', N'Samantha Green', NULL, N'AI Publications', CAST(N'2023-01-12' AS Date), N'9782233445566', N'10.1000/understandingAI', N'Article', N'A detailed overview of AI and its applications in modern technology.', 8, CAST(N'2025-02-07' AS Date), NULL)
INSERT [dbo].[Bibliographic_Materials] ([Material_ID], [Title], [Author], [Co_Authors], [Publisher], [Publication_Date], [ISBN], [DOI], [Material_Type], [Abstract], [Available_Copies], [Date_Added], [Times_Loaned]) VALUES (5, N'Introduction to JavaScript', N'David White', N'Emily Harris', N'Programming Press', CAST(N'2019-05-30' AS Date), N'9783344556677', N'10.1000/introJS', N'Book', N'Fundamentals of JavaScript programming language.', 3, CAST(N'2025-02-07' AS Date), NULL)
INSERT [dbo].[Bibliographic_Materials] ([Material_ID], [Title], [Author], [Co_Authors], [Publisher], [Publication_Date], [ISBN], [DOI], [Material_Type], [Abstract], [Available_Copies], [Date_Added], [Times_Loaned]) VALUES (6, N'Web Development Essentials', N'Christopher Scott', N'Anna Taylor', N'WebMaster Publications', CAST(N'2021-09-10' AS Date), N'9784455667788', N'10.1000/webdevessentials', N'Book', N'A complete guide to web development practices and tools.', 0, CAST(N'2025-02-07' AS Date), NULL)
INSERT [dbo].[Bibliographic_Materials] ([Material_ID], [Title], [Author], [Co_Authors], [Publisher], [Publication_Date], [ISBN], [DOI], [Material_Type], [Abstract], [Available_Copies], [Date_Added], [Times_Loaned]) VALUES (7, N'Database Management Systems', N'Robert King', NULL, N'TechWorld', CAST(N'2018-02-18' AS Date), N'9785566778899', N'10.1000/dbms', N'Book', N'An introduction to database management systems and their implementation.', 6, CAST(N'2025-02-07' AS Date), NULL)
INSERT [dbo].[Bibliographic_Materials] ([Material_ID], [Title], [Author], [Co_Authors], [Publisher], [Publication_Date], [ISBN], [DOI], [Material_Type], [Abstract], [Available_Copies], [Date_Added], [Times_Loaned]) VALUES (8, N'Quantum Computing Explained', N'Linda Turner', N'Michael Allen', N'Quantum Press', CAST(N'2024-04-05' AS Date), N'9786677889900', N'10.1000/quantumcomp', N'Book', N'A beginner\s guide to understanding quantum computing.', 1, CAST(N'2025-02-07' AS Date), NULL)
INSERT [dbo].[Bibliographic_Materials] ([Material_ID], [Title], [Author], [Co_Authors], [Publisher], [Publication_Date], [ISBN], [DOI], [Material_Type], [Abstract], [Available_Copies], [Date_Added], [Times_Loaned]) VALUES (9, N'Digital Signal Processing', N'James Stewart', NULL, N'SignalBooks', CAST(N'2022-08-21' AS Date), N'9787788990011', N'10.1000/dsp', N'Article', N'An overview of digital signal processing and its applications.', 8, CAST(N'2025-02-07' AS Date), NULL)
INSERT [dbo].[Bibliographic_Materials] ([Material_ID], [Title], [Author], [Co_Authors], [Publisher], [Publication_Date], [ISBN], [DOI], [Material_Type], [Abstract], [Available_Copies], [Date_Added], [Times_Loaned]) VALUES (10, N'Advanced Programming Techniques', N'Grace Lee', N'Paul Young', N'AdvancedTech', CAST(N'2020-11-09' AS Date), N'9788899001122', N'10.1000/advprogtech', N'Book', N'Detailed programming techniques for experienced developers.', 2, CAST(N'2025-02-07' AS Date), NULL)
INSERT [dbo].[Bibliographic_Materials] ([Material_ID], [Title], [Author], [Co_Authors], [Publisher], [Publication_Date], [ISBN], [DOI], [Material_Type], [Abstract], [Available_Copies], [Date_Added], [Times_Loaned]) VALUES (11, N'Cloud Computing for Beginners', N'Steven Walker', NULL, N'CloudPress', CAST(N'2021-03-17' AS Date), N'9789900112233', N'10.1000/cloudcomputing', N'Book', N'A primer on cloud computing and its services.', 2, CAST(N'2025-02-07' AS Date), NULL)
INSERT [dbo].[Bibliographic_Materials] ([Material_ID], [Title], [Author], [Co_Authors], [Publisher], [Publication_Date], [ISBN], [DOI], [Material_Type], [Abstract], [Available_Copies], [Date_Added], [Times_Loaned]) VALUES (12, N'Introduction to Data Science', N'Helen Adams', N'William Carter', N'Data Science Insights', CAST(N'2019-01-25' AS Date), N'9781011121314', N'10.1000/datascienceintro', N'Book', N'An introductory guide to data science concepts and tools.', 8, CAST(N'2025-02-07' AS Date), NULL)
INSERT [dbo].[Bibliographic_Materials] ([Material_ID], [Title], [Author], [Co_Authors], [Publisher], [Publication_Date], [ISBN], [DOI], [Material_Type], [Abstract], [Available_Copies], [Date_Added], [Times_Loaned]) VALUES (13, N'Cybersecurity Basics', N'Rachel Lee', NULL, N'SecureTech', CAST(N'2023-02-10' AS Date), N'9782122233445', N'10.1000/cybersecuritybasics', N'Article', N'Essential information on cybersecurity and protecting digital data.', 5, CAST(N'2025-02-07' AS Date), NULL)
INSERT [dbo].[Bibliographic_Materials] ([Material_ID], [Title], [Author], [Co_Authors], [Publisher], [Publication_Date], [ISBN], [DOI], [Material_Type], [Abstract], [Available_Copies], [Date_Added], [Times_Loaned]) VALUES (14, N'Operating Systems Concepts', N'Brian Scott', N'Karen Mitchell', N'OS Press', CAST(N'2020-06-14' AS Date), N'9783233444556', N'10.1000/osconcepts', N'Book', N'An overview of operating systems and their structure.', 0, CAST(N'2025-02-07' AS Date), NULL)
INSERT [dbo].[Bibliographic_Materials] ([Material_ID], [Title], [Author], [Co_Authors], [Publisher], [Publication_Date], [ISBN], [DOI], [Material_Type], [Abstract], [Available_Copies], [Date_Added], [Times_Loaned]) VALUES (15, N'Software Engineering Principles', N'Edward Harris', N'George Brown', N'Software Books', CAST(N'2022-11-03' AS Date), N'9784344556677', N'10.1000/softengprinciples', N'Book', N'Principles and methodologies used in software engineering.', 6, CAST(N'2025-02-07' AS Date), NULL)
INSERT [dbo].[Bibliographic_Materials] ([Material_ID], [Title], [Author], [Co_Authors], [Publisher], [Publication_Date], [ISBN], [DOI], [Material_Type], [Abstract], [Available_Copies], [Date_Added], [Times_Loaned]) VALUES (16, N'test', N'test', N'test', N'test', CAST(N'2025-02-12' AS Date), N'1212422', N'12525', N'To Kill a Mockingbird', NULL, 2, CAST(N'2025-02-12' AS Date), 0)
SET IDENTITY_INSERT [dbo].[Bibliographic_Materials] OFF
GO
SET IDENTITY_INSERT [dbo].[Clients] ON 

INSERT [dbo].[Clients] ([Client_ID], [First_Name], [Last_Name], [Date_of_Birth], [Email], [Phone], [Address], [Membership_Active]) VALUES (1, N'John', N'Doe', CAST(N'1990-05-15' AS Date), N'john.doe@example.com', N'123-456-7890', N'1234 Elm St, Springfield', 1)
INSERT [dbo].[Clients] ([Client_ID], [First_Name], [Last_Name], [Date_of_Birth], [Email], [Phone], [Address], [Membership_Active]) VALUES (2, N'Jane', N'Smith', CAST(N'1985-11-22' AS Date), N'jane.smith@example.com', N'987-654-3210', N'5678 Oak Rd, Springfield', 1)
INSERT [dbo].[Clients] ([Client_ID], [First_Name], [Last_Name], [Date_of_Birth], [Email], [Phone], [Address], [Membership_Active]) VALUES (3, N'Alice', N'Johnson', CAST(N'2025-02-19' AS Date), N'alice.johnson@example.com', N'555-123-4567', N'2345 Maple Ave, Shelbyville', 0)
INSERT [dbo].[Clients] ([Client_ID], [First_Name], [Last_Name], [Date_of_Birth], [Email], [Phone], [Address], [Membership_Active]) VALUES (4, N'Bob', N'Brown', CAST(N'1980-08-05' AS Date), N'bob.brown@example.com', N'777-222-1111', N'9876 Pine Blvd, Rivertown', 1)
INSERT [dbo].[Clients] ([Client_ID], [First_Name], [Last_Name], [Date_of_Birth], [Email], [Phone], [Address], [Membership_Active]) VALUES (5, N'Charlie', N'Davis', CAST(N'1995-01-25' AS Date), N'charlie.davis@example.com', N'888-999-0000', N'3456 Birch Ln, Lakeside', 1)
INSERT [dbo].[Clients] ([Client_ID], [First_Name], [Last_Name], [Date_of_Birth], [Email], [Phone], [Address], [Membership_Active]) VALUES (6, N'Emma', N'Martinez', CAST(N'1987-12-14' AS Date), N'emma.martinez@example.com', N'666-555-4444', N'4321 Cedar St, Hilltop', 0)
INSERT [dbo].[Clients] ([Client_ID], [First_Name], [Last_Name], [Date_of_Birth], [Email], [Phone], [Address], [Membership_Active]) VALUES (7, N'David', N'Lopez', CAST(N'1994-06-18' AS Date), N'david.lopez@example.com', N'555-666-7777', N'8765 Redwood Ave, Glenwood', 1)
INSERT [dbo].[Clients] ([Client_ID], [First_Name], [Last_Name], [Date_of_Birth], [Email], [Phone], [Address], [Membership_Active]) VALUES (8, N'Sophia', N'Gonzalez', CAST(N'1989-09-09' AS Date), N'sophia.gonzalez@example.com', N'444-333-2222', N'3457 Elm St, Baytown', 1)
INSERT [dbo].[Clients] ([Client_ID], [First_Name], [Last_Name], [Date_of_Birth], [Email], [Phone], [Address], [Membership_Active]) VALUES (9, N'Mia', N'Wilson', CAST(N'1993-02-10' AS Date), N'mia.wilson@example.com', N'333-222-1111', N'6543 Oak Ave, Greenfield', 1)
INSERT [dbo].[Clients] ([Client_ID], [First_Name], [Last_Name], [Date_of_Birth], [Email], [Phone], [Address], [Membership_Active]) VALUES (10, N'Daniel', N'Martinez', CAST(N'1991-07-30' AS Date), N'daniel.martinez@example.com', N'222-111-0000', N'1236 Birch St, Palm City', 1)
INSERT [dbo].[Clients] ([Client_ID], [First_Name], [Last_Name], [Date_of_Birth], [Email], [Phone], [Address], [Membership_Active]) VALUES (11, N'Lucas', N'Garcia', CAST(N'1984-11-02' AS Date), N'lucas.garcia@example.com', N'444-555-6666', N'8765 Maple Rd, Fortville', 0)
INSERT [dbo].[Clients] ([Client_ID], [First_Name], [Last_Name], [Date_of_Birth], [Email], [Phone], [Address], [Membership_Active]) VALUES (12, N'Amelia', N'Rodriguez', CAST(N'1996-04-17' AS Date), N'amelia.rodriguez@example.com', N'777-888-9999', N'2345 Pine Rd, Stonehill', 1)
INSERT [dbo].[Clients] ([Client_ID], [First_Name], [Last_Name], [Date_of_Birth], [Email], [Phone], [Address], [Membership_Active]) VALUES (13, N'James', N'Hernandez', CAST(N'1982-10-20' AS Date), N'james.hernandez@example.com', N'333-444-5555', N'6547 Redwood Blvd, Westfield', 0)
INSERT [dbo].[Clients] ([Client_ID], [First_Name], [Last_Name], [Date_of_Birth], [Email], [Phone], [Address], [Membership_Active]) VALUES (14, N'Benjamin', N'Lee', CAST(N'1990-12-11' AS Date), N'benjamin.lee@example.com', N'555-444-3333', N'4325 Oak Blvd, Brookhaven', 1)
INSERT [dbo].[Clients] ([Client_ID], [First_Name], [Last_Name], [Date_of_Birth], [Email], [Phone], [Address], [Membership_Active]) VALUES (15, N'Chloe', N'Walker', CAST(N'1994-05-05' AS Date), N'chloe.walker@example.com', N'666-777-8888', N'3458 Cedar Ln, Milltown', 1)
INSERT [dbo].[Clients] ([Client_ID], [First_Name], [Last_Name], [Date_of_Birth], [Email], [Phone], [Address], [Membership_Active]) VALUES (16, N'Shkelqim', N'Durmishi', CAST(N'2025-02-13' AS Date), N'shkelqimdurmishi02@gmail.com', N'049697927', N'oso kuka', 0)
INSERT [dbo].[Clients] ([Client_ID], [First_Name], [Last_Name], [Date_of_Birth], [Email], [Phone], [Address], [Membership_Active]) VALUES (17, N'Altin', N'Deliu', CAST(N'2025-02-18' AS Date), N'altindeliu@gmail.com', N'049499409', N'oso', 1)
INSERT [dbo].[Clients] ([Client_ID], [First_Name], [Last_Name], [Date_of_Birth], [Email], [Phone], [Address], [Membership_Active]) VALUES (18, N'Shkelqim', N'dsdsds', CAST(N'2025-01-29' AS Date), N'dsdsdsd', N'0499756', N'dsdsd', 1)
INSERT [dbo].[Clients] ([Client_ID], [First_Name], [Last_Name], [Date_of_Birth], [Email], [Phone], [Address], [Membership_Active]) VALUES (19, N'sasas', N'asasa', CAST(N'2025-02-21' AS Date), N'sasasasa', N'0496862', N'osososa', 0)
INSERT [dbo].[Clients] ([Client_ID], [First_Name], [Last_Name], [Date_of_Birth], [Email], [Phone], [Address], [Membership_Active]) VALUES (20, N'fsfsf', N'fsfsfsf', CAST(N'2025-02-05' AS Date), N'fsfsdfsfsfsdfs', N'049697927', N'fsfsfssfsf', 1)
INSERT [dbo].[Clients] ([Client_ID], [First_Name], [Last_Name], [Date_of_Birth], [Email], [Phone], [Address], [Membership_Active]) VALUES (21, N'Agon', N'Kadriu', CAST(N'2025-02-25' AS Date), N'agonkadriu022gmail.com', N'049697927', N'oso kuka', 1)
SET IDENTITY_INSERT [dbo].[Clients] OFF
GO
SET IDENTITY_INSERT [dbo].[Loans] ON 

INSERT [dbo].[Loans] ([Loan_ID], [Client_ID], [Material_ID], [Loan_Date], [Return_Date], [Actual_Return_Date], [Penalty_Fee]) VALUES (1, 1, 3, CAST(N'2025-02-01' AS Date), CAST(N'2025-02-15' AS Date), NULL, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([Loan_ID], [Client_ID], [Material_ID], [Loan_Date], [Return_Date], [Actual_Return_Date], [Penalty_Fee]) VALUES (2, 2, 1, CAST(N'2025-01-25' AS Date), CAST(N'2025-02-08' AS Date), NULL, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([Loan_ID], [Client_ID], [Material_ID], [Loan_Date], [Return_Date], [Actual_Return_Date], [Penalty_Fee]) VALUES (3, 3, 4, CAST(N'2025-01-30' AS Date), CAST(N'2025-02-13' AS Date), NULL, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([Loan_ID], [Client_ID], [Material_ID], [Loan_Date], [Return_Date], [Actual_Return_Date], [Penalty_Fee]) VALUES (4, 4, 5, CAST(N'2025-01-20' AS Date), CAST(N'2025-02-03' AS Date), NULL, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([Loan_ID], [Client_ID], [Material_ID], [Loan_Date], [Return_Date], [Actual_Return_Date], [Penalty_Fee]) VALUES (5, 5, 6, CAST(N'2025-02-01' AS Date), CAST(N'2025-02-15' AS Date), NULL, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([Loan_ID], [Client_ID], [Material_ID], [Loan_Date], [Return_Date], [Actual_Return_Date], [Penalty_Fee]) VALUES (6, 6, 2, CAST(N'2025-01-28' AS Date), CAST(N'2025-02-11' AS Date), NULL, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([Loan_ID], [Client_ID], [Material_ID], [Loan_Date], [Return_Date], [Actual_Return_Date], [Penalty_Fee]) VALUES (7, 7, 3, CAST(N'2025-02-05' AS Date), CAST(N'2025-02-19' AS Date), NULL, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([Loan_ID], [Client_ID], [Material_ID], [Loan_Date], [Return_Date], [Actual_Return_Date], [Penalty_Fee]) VALUES (8, 8, 7, CAST(N'2025-02-02' AS Date), CAST(N'2025-02-16' AS Date), NULL, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([Loan_ID], [Client_ID], [Material_ID], [Loan_Date], [Return_Date], [Actual_Return_Date], [Penalty_Fee]) VALUES (9, 9, 8, CAST(N'2025-01-22' AS Date), CAST(N'2025-02-05' AS Date), NULL, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([Loan_ID], [Client_ID], [Material_ID], [Loan_Date], [Return_Date], [Actual_Return_Date], [Penalty_Fee]) VALUES (10, 10, 10, CAST(N'2025-01-26' AS Date), CAST(N'2025-02-09' AS Date), NULL, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([Loan_ID], [Client_ID], [Material_ID], [Loan_Date], [Return_Date], [Actual_Return_Date], [Penalty_Fee]) VALUES (11, 11, 9, CAST(N'2025-02-03' AS Date), CAST(N'2025-02-17' AS Date), NULL, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([Loan_ID], [Client_ID], [Material_ID], [Loan_Date], [Return_Date], [Actual_Return_Date], [Penalty_Fee]) VALUES (12, 12, 12, CAST(N'2025-02-07' AS Date), CAST(N'2025-02-21' AS Date), NULL, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([Loan_ID], [Client_ID], [Material_ID], [Loan_Date], [Return_Date], [Actual_Return_Date], [Penalty_Fee]) VALUES (13, 13, 6, CAST(N'2025-01-24' AS Date), CAST(N'2025-02-07' AS Date), NULL, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([Loan_ID], [Client_ID], [Material_ID], [Loan_Date], [Return_Date], [Actual_Return_Date], [Penalty_Fee]) VALUES (14, 14, 11, CAST(N'2025-02-04' AS Date), CAST(N'2025-02-18' AS Date), NULL, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([Loan_ID], [Client_ID], [Material_ID], [Loan_Date], [Return_Date], [Actual_Return_Date], [Penalty_Fee]) VALUES (15, 15, 5, CAST(N'2025-01-31' AS Date), CAST(N'2025-02-14' AS Date), NULL, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([Loan_ID], [Client_ID], [Material_ID], [Loan_Date], [Return_Date], [Actual_Return_Date], [Penalty_Fee]) VALUES (17, 8, 5, CAST(N'2025-02-11' AS Date), CAST(N'2025-02-26' AS Date), NULL, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([Loan_ID], [Client_ID], [Material_ID], [Loan_Date], [Return_Date], [Actual_Return_Date], [Penalty_Fee]) VALUES (18, 14, 12, CAST(N'2025-02-17' AS Date), CAST(N'2025-02-19' AS Date), NULL, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([Loan_ID], [Client_ID], [Material_ID], [Loan_Date], [Return_Date], [Actual_Return_Date], [Penalty_Fee]) VALUES (19, 17, 11, CAST(N'2025-02-11' AS Date), CAST(N'2025-02-17' AS Date), NULL, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([Loan_ID], [Client_ID], [Material_ID], [Loan_Date], [Return_Date], [Actual_Return_Date], [Penalty_Fee]) VALUES (20, 18, 6, CAST(N'2025-02-13' AS Date), CAST(N'2025-02-27' AS Date), NULL, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([Loan_ID], [Client_ID], [Material_ID], [Loan_Date], [Return_Date], [Actual_Return_Date], [Penalty_Fee]) VALUES (21, 11, 4, CAST(N'2025-02-13' AS Date), CAST(N'2025-02-27' AS Date), NULL, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([Loan_ID], [Client_ID], [Material_ID], [Loan_Date], [Return_Date], [Actual_Return_Date], [Penalty_Fee]) VALUES (22, 9, 14, CAST(N'2025-02-13' AS Date), CAST(N'2025-02-27' AS Date), NULL, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([Loan_ID], [Client_ID], [Material_ID], [Loan_Date], [Return_Date], [Actual_Return_Date], [Penalty_Fee]) VALUES (23, 12, 10, CAST(N'2025-02-10' AS Date), CAST(N'2025-02-15' AS Date), NULL, CAST(1.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([Loan_ID], [Client_ID], [Material_ID], [Loan_Date], [Return_Date], [Actual_Return_Date], [Penalty_Fee]) VALUES (24, 18, 10, CAST(N'2025-02-08' AS Date), CAST(N'2025-02-12' AS Date), NULL, CAST(0.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Loans] OFF
GO
SET IDENTITY_INSERT [dbo].[Payments] ON 

INSERT [dbo].[Payments] ([Payment_ID], [Client_ID], [Amount], [Payment_Date], [Payment_Type]) VALUES (1, 1, CAST(10.00 AS Decimal(10, 2)), CAST(N'2025-02-01' AS Date), N'Credit Card')
INSERT [dbo].[Payments] ([Payment_ID], [Client_ID], [Amount], [Payment_Date], [Payment_Type]) VALUES (2, 2, CAST(5.00 AS Decimal(10, 2)), CAST(N'2025-01-25' AS Date), N'Cash')
INSERT [dbo].[Payments] ([Payment_ID], [Client_ID], [Amount], [Payment_Date], [Payment_Type]) VALUES (3, 3, CAST(2.50 AS Decimal(10, 2)), CAST(N'2025-01-30' AS Date), N'Debit Card')
INSERT [dbo].[Payments] ([Payment_ID], [Client_ID], [Amount], [Payment_Date], [Payment_Type]) VALUES (4, 4, CAST(15.00 AS Decimal(10, 2)), CAST(N'2025-01-20' AS Date), N'Credit Card')
INSERT [dbo].[Payments] ([Payment_ID], [Client_ID], [Amount], [Payment_Date], [Payment_Type]) VALUES (5, 5, CAST(7.00 AS Decimal(10, 2)), CAST(N'2025-02-01' AS Date), N'Cash')
INSERT [dbo].[Payments] ([Payment_ID], [Client_ID], [Amount], [Payment_Date], [Payment_Type]) VALUES (6, 6, CAST(3.00 AS Decimal(10, 2)), CAST(N'2025-01-28' AS Date), N'Debit Card')
INSERT [dbo].[Payments] ([Payment_ID], [Client_ID], [Amount], [Payment_Date], [Payment_Type]) VALUES (7, 7, CAST(8.00 AS Decimal(10, 2)), CAST(N'2025-02-05' AS Date), N'Credit Card')
INSERT [dbo].[Payments] ([Payment_ID], [Client_ID], [Amount], [Payment_Date], [Payment_Type]) VALUES (8, 8, CAST(10.50 AS Decimal(10, 2)), CAST(N'2025-02-02' AS Date), N'Cash')
INSERT [dbo].[Payments] ([Payment_ID], [Client_ID], [Amount], [Payment_Date], [Payment_Type]) VALUES (9, 9, CAST(4.00 AS Decimal(10, 2)), CAST(N'2025-01-22' AS Date), N'Debit Card')
INSERT [dbo].[Payments] ([Payment_ID], [Client_ID], [Amount], [Payment_Date], [Payment_Type]) VALUES (10, 10, CAST(12.00 AS Decimal(10, 2)), CAST(N'2025-01-26' AS Date), N'Credit Card')
INSERT [dbo].[Payments] ([Payment_ID], [Client_ID], [Amount], [Payment_Date], [Payment_Type]) VALUES (11, 11, CAST(6.00 AS Decimal(10, 2)), CAST(N'2025-02-03' AS Date), N'Cash')
INSERT [dbo].[Payments] ([Payment_ID], [Client_ID], [Amount], [Payment_Date], [Payment_Type]) VALUES (12, 12, CAST(9.50 AS Decimal(10, 2)), CAST(N'2025-02-07' AS Date), N'Debit Card')
INSERT [dbo].[Payments] ([Payment_ID], [Client_ID], [Amount], [Payment_Date], [Payment_Type]) VALUES (13, 13, CAST(11.00 AS Decimal(10, 2)), CAST(N'2025-01-24' AS Date), N'Credit Card')
INSERT [dbo].[Payments] ([Payment_ID], [Client_ID], [Amount], [Payment_Date], [Payment_Type]) VALUES (14, 14, CAST(14.00 AS Decimal(10, 2)), CAST(N'2025-02-04' AS Date), N'Cash')
INSERT [dbo].[Payments] ([Payment_ID], [Client_ID], [Amount], [Payment_Date], [Payment_Type]) VALUES (15, 15, CAST(20.00 AS Decimal(10, 2)), CAST(N'2025-01-31' AS Date), N'Debit Card')
INSERT [dbo].[Payments] ([Payment_ID], [Client_ID], [Amount], [Payment_Date], [Payment_Type]) VALUES (16, 18, CAST(4.00 AS Decimal(10, 2)), CAST(N'2025-02-13' AS Date), N'Pagesë mujore ')
INSERT [dbo].[Payments] ([Payment_ID], [Client_ID], [Amount], [Payment_Date], [Payment_Type]) VALUES (17, 11, CAST(3.00 AS Decimal(10, 2)), CAST(N'2025-02-18' AS Date), N'Cash')
INSERT [dbo].[Payments] ([Payment_ID], [Client_ID], [Amount], [Payment_Date], [Payment_Type]) VALUES (18, 9, CAST(1.00 AS Decimal(10, 2)), CAST(N'2025-02-14' AS Date), N'Debit Card')
SET IDENTITY_INSERT [dbo].[Payments] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([User_ID], [Username], [Password], [Role]) VALUES (1, N'Shkelqim', N'Shkelqim@', N'Administrator')
INSERT [dbo].[Users] ([User_ID], [Username], [Password], [Role]) VALUES (2, N'Altin', N'Altin@', N'Administrator')
INSERT [dbo].[Users] ([User_ID], [Username], [Password], [Role]) VALUES (3, N'Shkelqim@', N'Shkelqim', NULL)
INSERT [dbo].[Users] ([User_ID], [Username], [Password], [Role]) VALUES (4, N'Agon', N'Agon123', N'Biblotekar')
INSERT [dbo].[Users] ([User_ID], [Username], [Password], [Role]) VALUES (5, N'Albert', N'Albert123', N'Person i thjeshte')
INSERT [dbo].[Users] ([User_ID], [Username], [Password], [Role]) VALUES (6, N'rinor', N'rinor', N'Biblotekar')
INSERT [dbo].[Users] ([User_ID], [Username], [Password], [Role]) VALUES (7, N'Ajet', N'Ajet@', N'Person i thjeshte')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Bibliogr__447D36EA29AA650B]    Script Date: 2/20/2025 7:58:06 PM ******/
ALTER TABLE [dbo].[Bibliographic_Materials] ADD UNIQUE NONCLUSTERED 
(
	[ISBN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Clients__A9D105347D46A697]    Script Date: 2/20/2025 7:58:06 PM ******/
ALTER TABLE [dbo].[Clients] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__536C85E4FD901EE9]    Script Date: 2/20/2025 7:58:06 PM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bibliographic_Materials] ADD  DEFAULT (getdate()) FOR [Date_Added]
GO
ALTER TABLE [dbo].[Bibliographic_Materials] ADD  DEFAULT ((0)) FOR [Times_Loaned]
GO
ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((1)) FOR [Membership_Active]
GO
ALTER TABLE [dbo].[Loans] ADD  DEFAULT (getdate()) FOR [Loan_Date]
GO
ALTER TABLE [dbo].[Loans] ADD  DEFAULT ((0)) FOR [Penalty_Fee]
GO
ALTER TABLE [dbo].[Payments] ADD  DEFAULT (getdate()) FOR [Payment_Date]
GO
ALTER TABLE [dbo].[Loans]  WITH CHECK ADD FOREIGN KEY([Client_ID])
REFERENCES [dbo].[Clients] ([Client_ID])
GO
ALTER TABLE [dbo].[Loans]  WITH CHECK ADD FOREIGN KEY([Material_ID])
REFERENCES [dbo].[Bibliographic_Materials] ([Material_ID])
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD FOREIGN KEY([Client_ID])
REFERENCES [dbo].[Clients] ([Client_ID])
GO
ALTER TABLE [dbo].[Bibliographic_Materials]  WITH CHECK ADD CHECK  (([Available_Copies]>=(0)))
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD CHECK  (([Amount]>=(0)))
GO
/****** Object:  StoredProcedure [dbo].[AddPayment]    Script Date: 2/20/2025 7:58:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddPayment]
    @Client_ID INT,
    @Amount DECIMAL(10,2),
    @Payment_Type VARCHAR(50)
AS
BEGIN
    INSERT INTO Payments (Client_ID, Amount, Payment_Type) 
    VALUES (@Client_ID, @Amount, @Payment_Type);
END;
GO
/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 2/20/2025 7:58:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddUser]
    @Username NVARCHAR(50),
    @Password NVARCHAR(255),
    @Role NVARCHAR(50)
AS
BEGIN
    -- Shto një përdorues të ri në tabelë
    INSERT INTO Users (Username, Password, Role)
    VALUES (@Username, @Password, @Role);
END;
GO
/****** Object:  StoredProcedure [dbo].[CalculateLateFee]    Script Date: 2/20/2025 7:58:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CalculateLateFee]
    @Loan_ID INT
AS
BEGIN
    DECLARE @Return_Date DATE, @Actual_Return_Date DATE, @Days_Late INT, @Penalty DECIMAL(10,2);

    SELECT @Return_Date = Return_Date, @Actual_Return_Date = Actual_Return_Date
    FROM Loans WHERE Loan_ID = @Loan_ID;

    IF @Actual_Return_Date IS NULL OR @Actual_Return_Date <= @Return_Date
    BEGIN
        SET @Penalty = 0;
    END
    ELSE
    BEGIN
        SET @Days_Late = DATEDIFF(DAY, @Return_Date, @Actual_Return_Date);
        SET @Penalty = @Days_Late * 0.50; -- Shembull: 0.50€ për çdo ditë vonesë
    END

    UPDATE Loans SET Penalty_Fee = @Penalty WHERE Loan_ID = @Loan_ID;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetClientLoans]    Script Date: 2/20/2025 7:58:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetClientLoans]
    @Client_ID INT
AS
BEGIN
    SELECT L.Loan_ID, B.Title, L.Loan_Date, L.Return_Date, L.Actual_Return_Date, L.Penalty_Fee
    FROM Loans L
    INNER JOIN Bibliographic_Materials B ON L.Material_ID = B.Material_ID
    WHERE L.Client_ID = @Client_ID;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetMaterialByISBN]    Script Date: 2/20/2025 7:58:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetMaterialByISBN]
    @ISBN VARCHAR(13)
AS
BEGIN
    SELECT * FROM Bibliographic_Materials WHERE ISBN = @ISBN;
END;
GO
/****** Object:  StoredProcedure [dbo].[RegisterClient]    Script Date: 2/20/2025 7:58:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegisterClient]
    @First_Name VARCHAR(100),
    @Last_Name VARCHAR(100),
    @Date_of_Birth DATE,
    @Email VARCHAR(100),
    @Phone VARCHAR(20),
    @Address TEXT
AS
BEGIN
    INSERT INTO Clients (First_Name, Last_Name, Date_of_Birth, Email, Phone, Address, Membership_Active)
    VALUES (@First_Name, @Last_Name, @Date_of_Birth, @Email, @Phone, @Address, 1);
END;
GO
/****** Object:  StoredProcedure [dbo].[RegisterLoan]    Script Date: 2/20/2025 7:58:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegisterLoan]
    @Client_ID INT,
    @Material_ID INT,
    @Return_Date DATE
AS
BEGIN
    DECLARE @Available_Copies INT;
    
    SELECT @Available_Copies = Available_Copies FROM Bibliographic_Materials WHERE Material_ID = @Material_ID;
    
    IF @Available_Copies > 0
    BEGIN
        INSERT INTO Loans (Client_ID, Material_ID, Loan_Date, Return_Date) 
        VALUES (@Client_ID, @Material_ID, GETDATE(), @Return_Date);
        
        UPDATE Bibliographic_Materials SET Available_Copies = Available_Copies - 1 WHERE Material_ID = @Material_ID;
    END
    ELSE
    BEGIN
        PRINT 'Materiali nuk është i disponueshëm';
    END
END;
GO
USE [master]
GO
ALTER DATABASE [Bibloteka] SET  READ_WRITE 
GO

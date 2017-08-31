// ############# Run sql to script your tables #########################
// ############# your could also initialize using code first ###########

//USE[your db]
//GO
//SET ANSI_NULLS ON
//GO
//SET QUOTED_IDENTIFIER ON
//GO
//CREATE TABLE[dbo].[LoginUser](
//	[Id]
//[bigint] IDENTITY(1,1) NOT NULL,

//[UserName] [nvarchar](255) NOT NULL,

//[PasswordHash] [nvarchar](255) NOT NULL,

//[Email] [nvarchar](50) NULL,
//	[Phone]
//[nvarchar](50) NULL,
// CONSTRAINT[PK_LoginUser] PRIMARY KEY CLUSTERED
//(
//   [Id] ASC
//)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
//) ON[PRIMARY]

//GO
///****** Object:  Table [dbo].[UserToken]    Script Date: 2017/8/31 10:32:30 ******/
//SET ANSI_NULLS ON
//GO
//SET QUOTED_IDENTIFIER ON
//GO
//CREATE TABLE[dbo].[UserToken](
//	[AccessToken]
//[nvarchar](128) NOT NULL,

//[RefreshToken] [nvarchar](128) NOT NULL,

//[ExpireAt] [datetime2](7) NOT NULL,

//[UserId] [bigint]
//NOT NULL,

//[Id] [bigint] IDENTITY(1,1) NOT NULL,
//CONSTRAINT[PK_UserToken] PRIMARY KEY CLUSTERED
//(
//[Id] ASC
//)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
//) ON[PRIMARY]

//GO

//###########################################################



namespace Core
{
    public class Setup
    {
        /// <summary>
        /// If it is single sign on
        /// </summary>
        public const bool IsSSO = false;

        /// <summary>
        /// Token will be expire in how many minutes
        /// </summary>
        public const int TokenExpireMinutes = 60;
    }
}

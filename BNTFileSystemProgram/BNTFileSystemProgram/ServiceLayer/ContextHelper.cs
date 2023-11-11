using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ContextHelper
    {
        private static ApplicationDbContext dbContext;
        private static AuthorContext authorContext;
        private static FormatContext formatContext;
        private static GenreContext genreContext;
        private static TagContext tagContext;
        private static VideoContext videoContext;

        public static ApplicationDbContext GetDbContext()
        {
            if (dbContext == null)
            {
                SetDbContext();
            }

            return dbContext;
        }

        public static void SetDbContext()
        {
            dbContext = new ApplicationDbContext();
        }

        public static AuthorContext GetAuthorContext()
        {
            if (authorContext == null)
            {
                SetAuthorContext();
            }

            return authorContext;
        }

        public static void SetAuthorContext()
        {
            authorContext = new AuthorContext(GetDbContext());
        }

        public static FormatContext GetFormatContext()
        {
            if (formatContext == null)
            {
                SetFormatContext();
            }

            return formatContext;
        }

        public static void SetFormatContext()
        {
            formatContext = new FormatContext(GetDbContext());
        }

        public static GenreContext GetGenreContext()
        {
            if (genreContext == null)
            {
                SetGenreContext();
            }

            return genreContext;
        }

        public static void SetGenreContext()
        {
            genreContext = new GenreContext(GetDbContext());
        }

        public static TagContext GetTagContext()
        {
            if (tagContext == null)
            {
                SetTagContext();
            }

            return tagContext;
        }

        public static void SetTagContext()
        {
            tagContext = new TagContext(GetDbContext());
        }

        public static VideoContext GetVideoContext()
        {
            if (videoContext == null)
            {
                SetVideoContext();
            }

            return videoContext;
        }

        public static void SetVideoContext()
        {
            videoContext = new VideoContext(GetDbContext());
        }
    }
}

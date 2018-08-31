// <copyright file="ArticleTypeTests.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Enumerations.Tests.Integration.Tests.Nlm
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Common.Enumerations.Nlm;
    using ProcessingTools.Extensions;

    /// <summary>
    /// <see cref="ArticleType" /> Tests.
    /// </summary>
    [TestClass]
    public class ArticleTypeTests
    {
        /// <summary>
        /// <see cref="ArticleType" /> Abstract member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_AbstractMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("abstract", ArticleType.Abstract.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Addendum member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_AddendumMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("addendum", ArticleType.Addendum.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Announcement member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_AnnouncementMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("announcement", ArticleType.Announcement.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Article Commentary member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_ArticleCommentaryMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("article-commentary", ArticleType.ArticleCommentary.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Book Review member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_BookReviewMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("book-review", ArticleType.BookReview.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Books Received member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_BooksReceivedMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("books-received", ArticleType.BooksReceived.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Brief Report member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_BriefReportMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("brief-report", ArticleType.BriefReport.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Calendar member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_CalendarMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("calendar", ArticleType.Calendar.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Case Report member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_CaseReportMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("case-report", ArticleType.CaseReport.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Collection member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_CollectionMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("collection", ArticleType.Collection.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Correction member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_CorrectionMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("correction", ArticleType.Correction.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Discussion member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_DiscussionMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("discussion", ArticleType.Discussion.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Dissertation member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_DissertationMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("dissertation", ArticleType.Dissertation.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Editorial member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_EditorialMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("editorial", ArticleType.Editorial.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> In Brief member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_InBriefMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("in-brief", ArticleType.InBrief.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Introduction member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_IntroductionMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("introduction", ArticleType.Introduction.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Letter member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_LetterMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("letter", ArticleType.Letter.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Meeting Report member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_MeetingReportMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("meeting-report", ArticleType.MeetingReport.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> News member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_NewsMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("news", ArticleType.News.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Obituary member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_ObituaryMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("obituary", ArticleType.Obituary.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Oration member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_OrationMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("oration", ArticleType.Oration.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Partial Retraction member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_PartialRetractionMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("partial-retraction", ArticleType.PartialRetraction.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Product Review member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_ProductReviewMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("product-review", ArticleType.ProductReview.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Rapid Communication member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_RapidCommunicationMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("rapid-communication", ArticleType.RapidCommunication.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Reply member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_ReplyMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("reply", ArticleType.Reply.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Reprint member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_ReprintMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("reprint", ArticleType.Reprint.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Research Article member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_ResearchArticleMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("research-article", ArticleType.ResearchArticle.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Retraction member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_RetractionMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("retraction", ArticleType.Retraction.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Review Article member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_ReviewArticleMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("review-article", ArticleType.ReviewArticle.GetName());
        }

        /// <summary>
        /// <see cref="ArticleType" /> Translation member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ArticleType_TranslationMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("translation", ArticleType.Translation.GetName());
        }
    }
}

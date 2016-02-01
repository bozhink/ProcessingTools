namespace ProcessingTools.Nlm.Publishing.Tests.RegressionTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Infrastructure.Extensions;
    using ProcessingTools.Nlm.Publishing.Types;

    [TestClass]
    public class ArticleTypeTests
    {
        private const string UnexpectedValueMessage = "Unexpected Value.";

        [TestMethod]
        public void ArticleType_AbstractMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("abstract", ArticleType.Abstract.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_AddendumMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("addendum", ArticleType.Addendum.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_AnnouncementMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("announcement", ArticleType.Announcement.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_ArticleCommentaryMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("article-commentary", ArticleType.ArticleCommentary.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_BookReviewMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("book-review", ArticleType.BookReview.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_BooksReceivedMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("books-received", ArticleType.BooksReceived.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_BriefReportMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("brief-report", ArticleType.BriefReport.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_CalendarMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("calendar", ArticleType.Calendar.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_CaseReportMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("case-report", ArticleType.CaseReport.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_CollectionMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("collection", ArticleType.Collection.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_CorrectionMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("correction", ArticleType.Correction.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_DiscussionMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("discussion", ArticleType.Discussion.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_DissertationMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("dissertation", ArticleType.Dissertation.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_EditorialMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("editorial", ArticleType.Editorial.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_InBriefMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("in-brief", ArticleType.InBrief.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_IntroductionMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("introduction", ArticleType.Introduction.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_LetterMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("letter", ArticleType.Letter.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_MeetingReportMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("meeting-report", ArticleType.MeetingReport.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_NewsMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("news", ArticleType.News.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_ObituaryMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("obituary", ArticleType.Obituary.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_OrationMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("oration", ArticleType.Oration.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_PartialRetractionMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("partial-retraction", ArticleType.PartialRetraction.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_ProductReviewMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("product-review", ArticleType.ProductReview.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_RapidCommunicationMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("rapid-communication", ArticleType.RapidCommunication.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_ReplyMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("reply", ArticleType.Reply.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_ReprintMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("reprint", ArticleType.Reprint.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_ResearchArticleMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("research-article", ArticleType.ResearchArticle.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_RetractionMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("retraction", ArticleType.Retraction.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_ReviewArticleMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("review-article", ArticleType.ReviewArticle.GetValue(), "UnexpectedValueMessage");
        }

        [TestMethod]
        public void ArticleType_TranslationMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("translation", ArticleType.Translation.GetValue(), "UnexpectedValueMessage");
        }
    }
}
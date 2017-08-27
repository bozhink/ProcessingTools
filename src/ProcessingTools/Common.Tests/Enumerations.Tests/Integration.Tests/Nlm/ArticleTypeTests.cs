namespace ProcessingTools.Enumerations.Tests.Integration.Tests.Nlm
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Enumerations.Nlm;

    [TestClass]
    public class ArticleTypeTests
    {
        [TestMethod]
        public void ArticleType_AbstractMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("abstract", ArticleType.Abstract.GetName());
        }

        [TestMethod]
        public void ArticleType_AddendumMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("addendum", ArticleType.Addendum.GetName());
        }

        [TestMethod]
        public void ArticleType_AnnouncementMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("announcement", ArticleType.Announcement.GetName());
        }

        [TestMethod]
        public void ArticleType_ArticleCommentaryMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("article-commentary", ArticleType.ArticleCommentary.GetName());
        }

        [TestMethod]
        public void ArticleType_BookReviewMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("book-review", ArticleType.BookReview.GetName());
        }

        [TestMethod]
        public void ArticleType_BooksReceivedMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("books-received", ArticleType.BooksReceived.GetName());
        }

        [TestMethod]
        public void ArticleType_BriefReportMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("brief-report", ArticleType.BriefReport.GetName());
        }

        [TestMethod]
        public void ArticleType_CalendarMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("calendar", ArticleType.Calendar.GetName());
        }

        [TestMethod]
        public void ArticleType_CaseReportMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("case-report", ArticleType.CaseReport.GetName());
        }

        [TestMethod]
        public void ArticleType_CollectionMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("collection", ArticleType.Collection.GetName());
        }

        [TestMethod]
        public void ArticleType_CorrectionMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("correction", ArticleType.Correction.GetName());
        }

        [TestMethod]
        public void ArticleType_DiscussionMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("discussion", ArticleType.Discussion.GetName());
        }

        [TestMethod]
        public void ArticleType_DissertationMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("dissertation", ArticleType.Dissertation.GetName());
        }

        [TestMethod]
        public void ArticleType_EditorialMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("editorial", ArticleType.Editorial.GetName());
        }

        [TestMethod]
        public void ArticleType_InBriefMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("in-brief", ArticleType.InBrief.GetName());
        }

        [TestMethod]
        public void ArticleType_IntroductionMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("introduction", ArticleType.Introduction.GetName());
        }

        [TestMethod]
        public void ArticleType_LetterMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("letter", ArticleType.Letter.GetName());
        }

        [TestMethod]
        public void ArticleType_MeetingReportMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("meeting-report", ArticleType.MeetingReport.GetName());
        }

        [TestMethod]
        public void ArticleType_NewsMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("news", ArticleType.News.GetName());
        }

        [TestMethod]
        public void ArticleType_ObituaryMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("obituary", ArticleType.Obituary.GetName());
        }

        [TestMethod]
        public void ArticleType_OrationMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("oration", ArticleType.Oration.GetName());
        }

        [TestMethod]
        public void ArticleType_PartialRetractionMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("partial-retraction", ArticleType.PartialRetraction.GetName());
        }

        [TestMethod]
        public void ArticleType_ProductReviewMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("product-review", ArticleType.ProductReview.GetName());
        }

        [TestMethod]
        public void ArticleType_RapidCommunicationMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("rapid-communication", ArticleType.RapidCommunication.GetName());
        }

        [TestMethod]
        public void ArticleType_ReplyMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("reply", ArticleType.Reply.GetName());
        }

        [TestMethod]
        public void ArticleType_ReprintMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("reprint", ArticleType.Reprint.GetName());
        }

        [TestMethod]
        public void ArticleType_ResearchArticleMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("research-article", ArticleType.ResearchArticle.GetName());
        }

        [TestMethod]
        public void ArticleType_RetractionMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("retraction", ArticleType.Retraction.GetName());
        }

        [TestMethod]
        public void ArticleType_ReviewArticleMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("review-article", ArticleType.ReviewArticle.GetName());
        }

        [TestMethod]
        public void ArticleType_TranslationMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("translation", ArticleType.Translation.GetName());
        }
    }
}

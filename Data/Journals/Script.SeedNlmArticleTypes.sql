/*
Post-Deployment Script Template                            
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.        
 Use SQLCMD syntax to include a file in the post-deployment script.            
 Example:      :r .\myfile.sql                                
 Use SQLCMD syntax to reference a variable in the post-deployment script.        
 Example:      :setvar TableName MyTable                            
               SELECT * FROM [$(TableName)]                    
--------------------------------------------------------------------------------------
*/
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('abstract');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('addendum');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('announcement');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('article-commentary');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('book-review');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('books-received');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('brief-report');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('calendar');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('case-report');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('collection');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('correction');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('discussion');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('dissertation');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('editorial');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('in-brief');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('introduction');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('letter');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('meeting-report');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('news');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('obituary');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('oration');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('partial-retraction');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('product-review');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('rapid-communication');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('reply');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('reprint');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('research-article');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('retraction');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('review-article');
INSERT INTO Articles.NlmArticleTypes (Name) VALUES ('translation');
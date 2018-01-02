// <copyright file="Resources.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace System.Linq.Dynamic
{
    /// <summary>
    /// Resources
    /// </summary>
    internal static class Resources
    {
        /// <summary>
        /// Duplicate Identifier
        /// </summary>
        public const string DuplicateIdentifier = "The identifier '{0}' was defined more than once";

        /// <summary>
        /// Expression Type Mismatch
        /// </summary>
        public const string ExpressionTypeMismatch = "Expression of type '{0}' expected";

        /// <summary>
        /// Expression Expected
        /// </summary>
        public const string ExpressionExpected = "Expression expected";

        /// <summary>
        /// Invalid Character Literal
        /// </summary>
        public const string InvalidCharacterLiteral = "Character literal must contain exactly one character";

        /// <summary>
        /// Invalid Integer Literal
        /// </summary>
        public const string InvalidIntegerLiteral = "Invalid integer literal '{0}'";

        /// <summary>
        /// Invalid Real Literal
        /// </summary>
        public const string InvalidRealLiteral = "Invalid real literal '{0}'";

        /// <summary>
        /// Unknown Identifier
        /// </summary>
        public const string UnknownIdentifier = "Unknown identifier '{0}'";

        /// <summary>
        /// No It In Scope
        /// </summary>
        public const string NoItInScope = "No 'it' is in scope";

        /// <summary>
        /// Iif Requires Three Args
        /// </summary>
        public const string IifRequiresThreeArgs = "The 'iif' function requires three arguments";

        /// <summary>
        /// First Expression Must Be Bool
        /// </summary>
        public const string FirstExprMustBeBool = "The first expression must be of type 'Boolean'";

        /// <summary>
        /// Both Types Convert To Other
        /// </summary>
        public const string BothTypesConvertToOther = "Both of the types '{0}' and '{1}' convert to the other";

        /// <summary>
        /// Neither Type Converts To Other
        /// </summary>
        public const string NeitherTypeConvertsToOther = "Neither of the types '{0}' and '{1}' converts to the other";

        /// <summary>
        /// Missing As Clause
        /// </summary>
        public const string MissingAsClause = "Expression is missing an 'as' clause";

        /// <summary>
        /// Args Incompatible With Lambda
        /// </summary>
        public const string ArgsIncompatibleWithLambda = "Argument list incompatible with lambda expression";

        /// <summary>
        /// Type Has No Nullable Form
        /// </summary>
        public const string TypeHasNoNullableForm = "Type '{0}' has no nullable form";

        /// <summary>
        /// No Matching Constructor
        /// </summary>
        public const string NoMatchingConstructor = "No matching constructor in type '{0}'";

        /// <summary>
        /// Ambiguous Constructor Invocation
        /// </summary>
        public const string AmbiguousConstructorInvocation = "Ambiguous invocation of '{0}' constructor";

        /// <summary>
        /// Cannot Convert Value
        /// </summary>
        public const string CannotConvertValue = "A value of type '{0}' cannot be converted to type '{1}'";

        /// <summary>
        /// No Applicable Method
        /// </summary>
        public const string NoApplicableMethod = "No applicable method '{0}' exists in type '{1}'";

        /// <summary>
        /// Methods Are Inaccessible
        /// </summary>
        public const string MethodsAreInaccessible = "Methods on type '{0}' are not accessible";

        /// <summary>
        /// Method Is Void
        /// </summary>
        public const string MethodIsVoid = "Method '{0}' in type '{1}' does not return a value";

        /// <summary>
        /// Ambiguous Method Invocation
        /// </summary>
        public const string AmbiguousMethodInvocation = "Ambiguous invocation of method '{0}' in type '{1}'";

        /// <summary>
        /// Unknown Property Or Field
        /// </summary>
        public const string UnknownPropertyOrField = "No property or field '{0}' exists in type '{1}'";

        /// <summary>
        /// No Applicable Aggregate
        /// </summary>
        public const string NoApplicableAggregate = "No applicable aggregate method '{0}' exists";

        /// <summary>
        /// Cannot Index Multi Dim Array
        /// </summary>
        public const string CannotIndexMultiDimArray = "Indexing of multi-dimensional arrays is not supported";

        /// <summary>
        /// Invalid Index
        /// </summary>
        public const string InvalidIndex = "Array index must be an integer expression";

        /// <summary>
        /// No Applicable Indexer
        /// </summary>
        public const string NoApplicableIndexer = "No applicable indexer exists in type '{0}'";

        /// <summary>
        /// Ambiguous Indexer Invocation
        /// </summary>
        public const string AmbiguousIndexerInvocation = "Ambiguous invocation of indexer in type '{0}'";

        /// <summary>
        /// Incompatible Operand
        /// </summary>
        public const string IncompatibleOperand = "Operator '{0}' incompatible with operand type '{1}'";

        /// <summary>
        /// Incompatible Operands
        /// </summary>
        public const string IncompatibleOperands = "Operator '{0}' incompatible with operand types '{1}' and '{2}'";

        /// <summary>
        /// Unterminated String Literal
        /// </summary>
        public const string UnterminatedStringLiteral = "Unterminated string literal";

        /// <summary>
        /// Invalid Character
        /// </summary>
        public const string InvalidCharacter = "Syntax error '{0}'";

        /// <summary>
        /// Digit Expected
        /// </summary>
        public const string DigitExpected = "Digit expected";

        /// <summary>
        /// Syntax Error
        /// </summary>
        public const string SyntaxError = "Syntax error";

        /// <summary>
        /// Token Expected
        /// </summary>
        public const string TokenExpected = "{0} expected";

        /// <summary>
        /// Parse Exception Format
        /// </summary>
        public const string ParseExceptionFormat = "{0} (at index {1})";

        /// <summary>
        /// Colon Expected
        /// </summary>
        public const string ColonExpected = "':' expected";

        /// <summary>
        /// Open Parenthesis Expected
        /// </summary>
        public const string OpenParenthesisExpected = "'(' expected";

        /// <summary>
        /// Close Parenthesis Or Operator Expected
        /// </summary>
        public const string CloseParenthesisOrOperatorExpected = "')' or operator expected";

        /// <summary>
        /// Close Parenthesis Or Comma Expected
        /// </summary>
        public const string CloseParenthesisOrCommaExpected = "')' or ',' expected";

        /// <summary>
        /// Dot Or Open Parenthesis Expected
        /// </summary>
        public const string DotOrOpenParenthesisExpected = "'.' or '(' expected";

        /// <summary>
        /// Open Bracket Expected
        /// </summary>
        public const string OpenBracketExpected = "'[' expected";

        /// <summary>
        /// Close Bracket Or Comma Expected
        /// </summary>
        public const string CloseBracketOrCommaExpected = "']' or ',' expected";

        /// <summary>
        /// Identifier Expected
        /// </summary>
        public const string IdentifierExpected = "Identifier expected";
    }
}

// <copyright file="Resources.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Linq.Dynamic
{
    /// <summary>
    /// Resources.
    /// </summary>
    internal static class Resources
    {
        /// <summary>
        /// Duplicate identifier.
        /// </summary>
        public const string DuplicateIdentifier = "The identifier '{0}' was defined more than once";

        /// <summary>
        /// Expression type mismatch.
        /// </summary>
        public const string ExpressionTypeMismatch = "Expression of type '{0}' expected";

        /// <summary>
        /// Expression expected.
        /// </summary>
        public const string ExpressionExpected = "Expression expected";

        /// <summary>
        /// Invalid character literal.
        /// </summary>
        public const string InvalidCharacterLiteral = "Character literal must contain exactly one character";

        /// <summary>
        /// Invalid .
        /// </summary>
        public const string InvalidIntegerLiteral = "Invalid integer literal '{0}'";

        /// <summary>
        /// Invalid real literal.
        /// </summary>
        public const string InvalidRealLiteral = "Invalid real literal '{0}'";

        /// <summary>
        /// Unknown .
        /// </summary>
        public const string UnknownIdentifier = "Unknown identifier '{0}'";

        /// <summary>
        /// No it in scope.
        /// </summary>
        public const string NoItInScope = "No 'it' is in scope";

        /// <summary>
        /// Iif requires three args.
        /// </summary>
        public const string IifRequiresThreeArgs = "The 'iif' function requires three arguments";

        /// <summary>
        /// First expression must be bool.
        /// </summary>
        public const string FirstExprMustBeBool = "The first expression must be of type 'Boolean'";

        /// <summary>
        /// Both types convert to other.
        /// </summary>
        public const string BothTypesConvertToOther = "Both of the types '{0}' and '{1}' convert to the other";

        /// <summary>
        /// Neither type converts to other.
        /// </summary>
        public const string NeitherTypeConvertsToOther = "Neither of the types '{0}' and '{1}' converts to the other";

        /// <summary>
        /// Missing as clause.
        /// </summary>
        public const string MissingAsClause = "Expression is missing an 'as' clause";

        /// <summary>
        /// Args incompatible with lambda.
        /// </summary>
        public const string ArgsIncompatibleWithLambda = "Argument list incompatible with lambda expression";

        /// <summary>
        /// Type has no nullable form.
        /// </summary>
        public const string TypeHasNoNullableForm = "Type '{0}' has no nullable form";

        /// <summary>
        /// No matching constructor.
        /// </summary>
        public const string NoMatchingConstructor = "No matching constructor in type '{0}'";

        /// <summary>
        /// Ambiguous constructor invocation.
        /// </summary>
        public const string AmbiguousConstructorInvocation = "Ambiguous invocation of '{0}' constructor";

        /// <summary>
        /// Cannot convert value.
        /// </summary>
        public const string CannotConvertValue = "A value of type '{0}' cannot be converted to type '{1}'";

        /// <summary>
        /// No applicable method.
        /// </summary>
        public const string NoApplicableMethod = "No applicable method '{0}' exists in type '{1}'";

        /// <summary>
        /// Methods are inaccessible.
        /// </summary>
        public const string MethodsAreInaccessible = "Methods on type '{0}' are not accessible";

        /// <summary>
        /// Method is void.
        /// </summary>
        public const string MethodIsVoid = "Method '{0}' in type '{1}' does not return a value";

        /// <summary>
        /// Ambiguous method invocation.
        /// </summary>
        public const string AmbiguousMethodInvocation = "Ambiguous invocation of method '{0}' in type '{1}'";

        /// <summary>
        /// Unknown property or field.
        /// </summary>
        public const string UnknownPropertyOrField = "No property or field '{0}' exists in type '{1}'";

        /// <summary>
        /// No applicable aggregate.
        /// </summary>
        public const string NoApplicableAggregate = "No applicable aggregate method '{0}' exists";

        /// <summary>
        /// Cannot index multi dim array.
        /// </summary>
        public const string CannotIndexMultiDimArray = "Indexing of multi-dimensional arrays is not supported";

        /// <summary>
        /// Invalid index.
        /// </summary>
        public const string InvalidIndex = "Array index must be an integer expression";

        /// <summary>
        /// No applicable indexer.
        /// </summary>
        public const string NoApplicableIndexer = "No applicable indexer exists in type '{0}'";

        /// <summary>
        /// Ambiguous indexer invocation.
        /// </summary>
        public const string AmbiguousIndexerInvocation = "Ambiguous invocation of indexer in type '{0}'";

        /// <summary>
        /// Incompatible operand.
        /// </summary>
        public const string IncompatibleOperand = "Operator '{0}' incompatible with operand type '{1}'";

        /// <summary>
        /// Incompatible operands.
        /// </summary>
        public const string IncompatibleOperands = "Operator '{0}' incompatible with operand types '{1}' and '{2}'";

        /// <summary>
        /// Unterminated string literal.
        /// </summary>
        public const string UnterminatedStringLiteral = "Unterminated string literal";

        /// <summary>
        /// Invalid character.
        /// </summary>
        public const string InvalidCharacter = "Syntax error '{0}'";

        /// <summary>
        /// Digit expected.
        /// </summary>
        public const string DigitExpected = "Digit expected";

        /// <summary>
        /// Syntax error.
        /// </summary>
        public const string SyntaxError = "Syntax error";

        /// <summary>
        /// Token expected.
        /// </summary>
        public const string TokenExpected = "{0} expected";

        /// <summary>
        /// Parse exception format.
        /// </summary>
        public const string ParseExceptionFormat = "{0} (at index {1})";

        /// <summary>
        /// Colon expected.
        /// </summary>
        public const string ColonExpected = "':' expected";

        /// <summary>
        /// Open parenthesis expected.
        /// </summary>
        public const string OpenParenthesisExpected = "'(' expected";

        /// <summary>
        /// Close parenthesis or operator expected.
        /// </summary>
        public const string CloseParenthesisOrOperatorExpected = "')' or operator expected";

        /// <summary>
        /// Close parenthesis or comma expected.
        /// </summary>
        public const string CloseParenthesisOrCommaExpected = "')' or ',' expected";

        /// <summary>
        /// Dot or open parenthesis expected.
        /// </summary>
        public const string DotOrOpenParenthesisExpected = "'.' or '(' expected";

        /// <summary>
        /// Open bracket expected.
        /// </summary>
        public const string OpenBracketExpected = "'[' expected";

        /// <summary>
        /// Close bracket or comma expected.
        /// </summary>
        public const string CloseBracketOrCommaExpected = "']' or ',' expected";

        /// <summary>
        /// Identifier expected.
        /// </summary>
        public const string IdentifierExpected = "Identifier expected";
    }
}

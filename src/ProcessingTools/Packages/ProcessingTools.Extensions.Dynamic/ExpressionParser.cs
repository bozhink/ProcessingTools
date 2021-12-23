// <copyright file="ExpressionParser.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Expression parser.
    /// </summary>
    internal class ExpressionParser
    {
        private const string KeywordIif = "iif";

        private const string KeywordIt = "it";

        private const string KeywordNew = "new";

        private const string KeywordOuterIt = "outerIt";

        private static readonly Expression FalseLiteral = Expression.Constant(false);

        private static readonly Expression NullLiteral = Expression.Constant(null);

        private static readonly Type[] PredefinedTypes =
        {
            typeof(object),
            typeof(bool),
            typeof(char),
            typeof(string),
            typeof(sbyte),
            typeof(byte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(TimeSpan),
            typeof(Guid),
            typeof(Math),
            typeof(Convert),
        };

        private static readonly Expression TrueLiteral = Expression.Constant(true);

        private static readonly Lazy<Dictionary<string, object>> KeywordsLazy = new Lazy<Dictionary<string, object>>(() => CreateKeywords());

        private readonly Dictionary<Expression, string> literals;

        private readonly Dictionary<string, object> symbols;

        private readonly string text;

        private readonly int textLen;

        private int textPos;

        private Token token;

        private char ch;

        private IDictionary<string, object> externals;

        private ParameterExpression it;

        private ParameterExpression outerIt;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParser"/> class.
        /// </summary>
        /// <param name="parameters">Expression parameters.</param>
        /// <param name="expression">Expression as string.</param>
        /// <param name="values">Expression values.</param>
        public ExpressionParser(ParameterExpression[] parameters, string expression, object[] values)
        {
            this.symbols = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            this.literals = new Dictionary<Expression, string>();
            if (parameters != null)
            {
                this.ProcessParameters(parameters);
            }

            if (values != null)
            {
                this.ProcessValues(values);
            }

            this.text = expression ?? throw new ArgumentNullException(nameof(expression));
            this.textLen = this.text.Length;
            this.SetTextPos(0);
            this.NextToken();
        }

        private enum TokenId
        {
            Unknown,
            End,
            Identifier,
            StringLiteral,
            IntegerLiteral,
            RealLiteral,
            Exclamation,
            Percent,
            Amphersand,
            OpenParen,
            CloseParen,
            Asterisk,
            Plus,
            Comma,
            Minus,
            Dot,
            Slash,
            Colon,
            LessThan,
            Equal,
            GreaterThan,
            Question,
            OpenBracket,
            CloseBracket,
            Bar,
            ExclamationEqual,
            DoubleAmphersand,
            LessThanEqual,
            LessGreater,
            DoubleEqual,
            GreaterThanEqual,
            DoubleBar,
        }

        /// <summary>
        /// Add Signatures.
        /// </summary>
        private interface IAddSignatures : IArithmeticSignatures
        {
            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(DateTime x, TimeSpan y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(TimeSpan x, TimeSpan y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(DateTime? x, TimeSpan? y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(TimeSpan? x, TimeSpan? y);
        }

        /// <summary>
        /// Arithmetic Signatures.
        /// </summary>
        private interface IArithmeticSignatures
        {
            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(int x, int y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(uint x, uint y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(long x, long y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(ulong x, ulong y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(float x, float y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(double x, double y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(decimal x, decimal y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(int? x, int? y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(uint? x, uint? y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(long? x, long? y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(ulong? x, ulong? y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(float? x, float? y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(double? x, double? y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(decimal? x, decimal? y);
        }

        /// <summary>
        /// Enumerable Signatures.
        /// </summary>
        private interface IEnumerableSignatures
        {
            /// <summary>
            /// All.
            /// </summary>
            /// <param name="predicate">predicate.</param>
            void All(bool predicate);

            /// <summary>
            /// Any.
            /// </summary>
            void Any();

            /// <summary>
            /// Any.
            /// </summary>
            /// <param name="predicate">predicate.</param>
            void Any(bool predicate);

            /// <summary>
            /// Average.
            /// </summary>
            /// <param name="selector">selector.</param>
            void Average(int selector);

            /// <summary>
            /// Average.
            /// </summary>
            /// <param name="selector">selector.</param>
            void Average(int? selector);

            /// <summary>
            /// Average.
            /// </summary>
            /// <param name="selector">selector.</param>
            void Average(long selector);

            /// <summary>
            /// Average.
            /// </summary>
            /// <param name="selector">selector.</param>
            void Average(long? selector);

            /// <summary>
            /// Average.
            /// </summary>
            /// <param name="selector">selector.</param>
            void Average(float selector);

            /// <summary>
            /// Average.
            /// </summary>
            /// <param name="selector">selector.</param>
            void Average(float? selector);

            /// <summary>
            /// Average.
            /// </summary>
            /// <param name="selector">selector.</param>
            void Average(double selector);

            /// <summary>
            /// Average.
            /// </summary>
            /// <param name="selector">selector.</param>
            void Average(double? selector);

            /// <summary>
            /// Average.
            /// </summary>
            /// <param name="selector">selector.</param>
            void Average(decimal selector);

            /// <summary>
            /// Average.
            /// </summary>
            /// <param name="selector">selector.</param>
            void Average(decimal? selector);

            /// <summary>
            /// Contains.
            /// </summary>
            /// <param name="selector">selector.</param>
            /// <returns>Boolean.</returns>
            bool Contains(object selector);

            /// <summary>
            /// Count.
            /// </summary>
            void Count();

            /// <summary>
            /// Count.
            /// </summary>
            /// <param name="predicate">predicate.</param>
            void Count(bool predicate);

            /// <summary>
            /// Max.
            /// </summary>
            /// <param name="selector">selector.</param>
            void Max(object selector);

            /// <summary>
            /// Min.
            /// </summary>
            /// <param name="selector">selector.</param>
            void Min(object selector);

            /// <summary>
            /// Sum.
            /// </summary>
            /// <param name="selector">selector.</param>
            void Sum(int selector);

            /// <summary>
            /// Sum.
            /// </summary>
            /// <param name="selector">selector.</param>
            void Sum(int? selector);

            /// <summary>
            /// Sum.
            /// </summary>
            /// <param name="selector">selector.</param>
            void Sum(long selector);

            /// <summary>
            /// Sum.
            /// </summary>
            /// <param name="selector">selector.</param>
            void Sum(long? selector);

            /// <summary>
            /// Sum.
            /// </summary>
            /// <param name="selector">selector.</param>
            void Sum(float selector);

            /// <summary>
            /// Sum.
            /// </summary>
            /// <param name="selector">selector.</param>
            void Sum(float? selector);

            /// <summary>
            /// Sum.
            /// </summary>
            /// <param name="selector">selector.</param>
            void Sum(double selector);

            /// <summary>
            /// Sum.
            /// </summary>
            /// <param name="selector">selector.</param>
            void Sum(double? selector);

            /// <summary>
            /// Sum.
            /// </summary>
            /// <param name="selector">selector.</param>
            void Sum(decimal selector);

            /// <summary>
            /// Sum.
            /// </summary>
            /// <param name="selector">selector.</param>
            void Sum(decimal? selector);

            /// <summary>
            /// Where.
            /// </summary>
            /// <param name="predicate">predicate.</param>
            void Where(bool predicate);
        }

        /// <summary>
        /// Equality Signatures.
        /// </summary>
        private interface IEqualitySignatures : IRelationalSignatures
        {
            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(bool x, bool y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(bool? x, bool? y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(Guid x, Guid y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(Guid? x, Guid? y);
        }

        /// <summary>
        /// Logical Signatures.
        /// </summary>
        private interface ILogicalSignatures
        {
            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(bool x, bool y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(bool? x, bool? y);
        }

        /// <summary>
        /// Negation Signatures.
        /// </summary>
        private interface INegationSignatures
        {
            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            void F(int x);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            void F(long x);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            void F(float x);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            void F(double x);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            void F(decimal x);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            void F(int? x);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            void F(long? x);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            void F(float? x);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            void F(double? x);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            void F(decimal? x);
        }

        /// <summary>
        /// Not Signatures.
        /// </summary>
        private interface INotSignatures
        {
            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            void F(bool x);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            void F(bool? x);
        }

        /// <summary>
        /// Relational Signatures.
        /// </summary>
        private interface IRelationalSignatures : IArithmeticSignatures
        {
            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(string x, string y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(char x, char y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(DateTime x, DateTime y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(DateTimeOffset x, DateTimeOffset y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(TimeSpan x, TimeSpan y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(char? x, char? y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(DateTime? x, DateTime? y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(DateTimeOffset? x, DateTimeOffset? y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(TimeSpan? x, TimeSpan? y);
        }

        /// <summary>
        /// Subtract Signatures.
        /// </summary>
        private interface ISubtractSignatures : IAddSignatures
        {
            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(DateTime x, DateTime y);

            /// <summary>
            /// F.
            /// </summary>
            /// <param name="x">x.</param>
            /// <param name="y">y.</param>
            void F(DateTime? x, DateTime? y);
        }

        private static Dictionary<string, object> Keywords => KeywordsLazy.Value;

        /// <summary>
        /// Parses expression.
        /// </summary>
        /// <param name="resultType">Type of the expression result.</param>
        /// <returns>Parsed expression.</returns>
        public Expression Parse(Type resultType)
        {
            int exprPos = this.token.Pos;
            Expression expr = this.ParseExpression();
            if (resultType != null)
            {
                expr = this.PromoteExpression(expr, resultType, true);
                if (expr == null)
                {
                    throw this.ParseError(exprPos, Resources.ExpressionTypeMismatch, GetTypeName(resultType));
                }
            }

            this.ValidateToken(TokenId.End, Resources.SyntaxError);
            return expr;
        }

        /// <summary>
        /// Parse ordering.
        /// </summary>
        /// <returns>Parsed ordering.</returns>
        public IEnumerable<DynamicOrdering> ParseOrdering()
        {
            List<DynamicOrdering> orderings = new List<DynamicOrdering>();
            while (true)
            {
                Expression expr = this.ParseExpression();
                bool ascending = true;
                if (this.TokenIdentifierIs("asc") || this.TokenIdentifierIs("ascending"))
                {
                    this.NextToken();
                }
                else if (this.TokenIdentifierIs("desc") || this.TokenIdentifierIs("descending"))
                {
                    this.NextToken();
                    ascending = false;
                }

                orderings.Add(new DynamicOrdering { Selector = expr, Ascending = ascending });
                if (this.token.Id != TokenId.Comma)
                {
                    break;
                }

                this.NextToken();
            }

            this.ValidateToken(TokenId.End, Resources.SyntaxError);
            return orderings;
        }

        private static void AddInterface(List<Type> types, Type type)
        {
            if (!types.Contains(type))
            {
                types.Add(type);
                foreach (Type t in type.GetInterfaces())
                {
                    AddInterface(types, t);
                }
            }
        }

        // Return 1 if s -> t1 is a better conversion than s -> t2
        // Return -1 if s -> t2 is a better conversion than s -> t1
        // Return 0 if neither conversion is better
        private static int CompareConversions(Type s, Type t1, Type t2)
        {
            if (t1 == t2)
            {
                return 0;
            }

            if (s == t1)
            {
                return 1;
            }

            if (s == t2)
            {
                return -1;
            }

            bool t1t2 = IsCompatibleWith(t1, t2);
            bool t2t1 = IsCompatibleWith(t2, t1);
            if (t1t2 && !t2t1)
            {
                return 1;
            }

            if (t2t1 && !t1t2)
            {
                return -1;
            }

            if (IsSignedIntegralType(t1) && IsUnsignedIntegralType(t2))
            {
                return 1;
            }

            if (IsSignedIntegralType(t2) && IsUnsignedIntegralType(t1))
            {
                return -1;
            }

            return 0;
        }

        private static Dictionary<string, object> CreateKeywords()
        {
            var d = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                { "true", TrueLiteral },
                { "false", FalseLiteral },
                { "null", NullLiteral },
                { KeywordIt, KeywordIt },
                { KeywordOuterIt, KeywordOuterIt },
                { KeywordIif, KeywordIif },
                { KeywordNew, KeywordNew },
            };

            foreach (Type type in PredefinedTypes)
            {
                d.Add(type.Name, type);
            }

            return d;
        }

        private static Type FindGenericType(Type generic, Type type)
        {
            while (type != null && type != typeof(object))
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == generic)
                {
                    return type;
                }

                if (generic.IsInterface)
                {
                    foreach (Type intfType in type.GetInterfaces())
                    {
                        Type found = FindGenericType(generic, intfType);
                        if (found != null)
                        {
                            return found;
                        }
                    }
                }

                type = type.BaseType;
            }

            return null;
        }

        private static Type GetNonNullableType(Type type)
        {
            return IsNullableType(type) ? type.GetGenericArguments()[0] : type;
        }

        private static int GetNumericTypeKind(Type type)
        {
            type = GetNonNullableType(type);
            if (type.IsEnum)
            {
                return 0;
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Char:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return 1;

                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return 2;

                case TypeCode.Byte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return 3;

                default:
                    return 0;
            }
        }

        private static string GetTypeName(Type type)
        {
            Type baseType = GetNonNullableType(type);
            string s = baseType.Name;
            if (type != baseType)
            {
                s += '?';
            }

            return s;
        }

        private static bool IsBetterThan(Expression[] args, MethodData m1, MethodData m2)
        {
            bool better = false;
            for (int i = 0; i < args.Length; i++)
            {
                int c = CompareConversions(
                    args[i].Type,
                    m1.Parameters[i].ParameterType,
                    m2.Parameters[i].ParameterType);

                if (c < 0)
                {
                    return false;
                }

                if (c > 0)
                {
                    better = true;
                }
            }

            return better;
        }

        private static bool IsCompatibleWith(Type source, Type target)
        {
            if (source == target)
            {
                return true;
            }

            if (!target.IsValueType)
            {
                return target.IsAssignableFrom(source);
            }

            Type st = GetNonNullableType(source);
            Type tt = GetNonNullableType(target);
            if (st != source && tt == target)
            {
                return false;
            }

            TypeCode sc = st.IsEnum ? TypeCode.Object : Type.GetTypeCode(st);
            TypeCode tc = tt.IsEnum ? TypeCode.Object : Type.GetTypeCode(tt);

            if (st.IsEnum && !tt.IsEnum)
            {
                // If the source is an enum and the target is numeric
                switch (tc)
                {
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                        return true;

                    default:
                        break;
                }
            }

            switch (sc)
            {
                case TypeCode.SByte:
                    switch (tc)
                    {
                        case TypeCode.SByte:
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;

                        default:
                            break;
                    }

                    break;

                case TypeCode.Byte:
                    switch (tc)
                    {
                        case TypeCode.Byte:
                        case TypeCode.Int16:
                        case TypeCode.UInt16:
                        case TypeCode.Int32:
                        case TypeCode.UInt32:
                        case TypeCode.Int64:
                        case TypeCode.UInt64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;

                        default:
                            break;
                    }

                    break;

                case TypeCode.Int16:
                    switch (tc)
                    {
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;

                        default:
                            break;
                    }

                    break;

                case TypeCode.UInt16:
                    switch (tc)
                    {
                        case TypeCode.UInt16:
                        case TypeCode.Int32:
                        case TypeCode.UInt32:
                        case TypeCode.Int64:
                        case TypeCode.UInt64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;

                        default:
                            break;
                    }

                    break;

                case TypeCode.Int32:
                    switch (tc)
                    {
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;

                        default:
                            break;
                    }

                    break;

                case TypeCode.UInt32:
                    switch (tc)
                    {
                        case TypeCode.UInt32:
                        case TypeCode.Int64:
                        case TypeCode.UInt64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;

                        default:
                            break;
                    }

                    break;

                case TypeCode.Int64:
                    switch (tc)
                    {
                        case TypeCode.Int64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;

                        default:
                            break;
                    }

                    break;

                case TypeCode.UInt64:
                    switch (tc)
                    {
                        case TypeCode.UInt64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;

                        default:
                            break;
                    }

                    break;

                case TypeCode.Single:
                    switch (tc)
                    {
                        case TypeCode.Single:
                        case TypeCode.Double:
                            return true;

                        default:
                            break;
                    }

                    break;

                default:
                    if (st == tt)
                    {
                        return true;
                    }

                    break;
            }

            return false;
        }

        private static bool IsEnumType(Type type)
        {
            return GetNonNullableType(type).IsEnum;
        }

        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        private static bool IsNumericType(Type type)
        {
            return GetNumericTypeKind(type) != 0;
        }

        private static bool IsSignedIntegralType(Type type)
        {
            return GetNumericTypeKind(type) == 2;
        }

        private static bool IsUnsignedIntegralType(Type type)
        {
            return GetNumericTypeKind(type) == 3;
        }

        private static object ParseEnum(string name, Type type)
        {
            if (type.IsEnum)
            {
                MemberInfo[] memberInfos = type.FindMembers(
                    MemberTypes.Field,
                    BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static,
                    Type.FilterNameIgnoreCase,
                    name);

                if (memberInfos.Length != 0)
                {
                    return ((FieldInfo)memberInfos[0]).GetValue(null);
                }
            }

            return null;
        }

        private static object ParseNumber(string text, Type type)
        {
            switch (Type.GetTypeCode(GetNonNullableType(type)))
            {
                case TypeCode.SByte:
                    sbyte sb;
                    if (sbyte.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out sb))
                    {
                        return sb;
                    }

                    break;

                case TypeCode.Byte:
                    byte b;
                    if (byte.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out b))
                    {
                        return b;
                    }

                    break;

                case TypeCode.Int16:
                    short s;
                    if (short.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out s))
                    {
                        return s;
                    }

                    break;

                case TypeCode.UInt16:
                    ushort us;
                    if (ushort.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out us))
                    {
                        return us;
                    }

                    break;

                case TypeCode.Int32:
                    int i;
                    if (int.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out i))
                    {
                        return i;
                    }

                    break;

                case TypeCode.UInt32:
                    uint ui;
                    if (uint.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out ui))
                    {
                        return ui;
                    }

                    break;

                case TypeCode.Int64:
                    long l;
                    if (long.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out l))
                    {
                        return l;
                    }

                    break;

                case TypeCode.UInt64:
                    ulong ul;
                    if (ulong.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out ul))
                    {
                        return ul;
                    }

                    break;

                case TypeCode.Single:
                    float f;
                    if (float.TryParse(text, NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.InvariantInfo, out f))
                    {
                        return f;
                    }

                    break;

                case TypeCode.Double:
                    double d;
                    if (double.TryParse(text, NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.InvariantInfo, out d))
                    {
                        return d;
                    }

                    break;

                case TypeCode.Decimal:
                    decimal e;
                    if (decimal.TryParse(text, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out e))
                    {
                        return e;
                    }

                    break;
            }

            return null;
        }

        private static IEnumerable<Type> SelfAndBaseClasses(Type type)
        {
            while (type != null)
            {
                yield return type;
                type = type.BaseType;
            }
        }

        private static IEnumerable<Type> SelfAndBaseTypes(Type type)
        {
            if (type.IsInterface)
            {
                List<Type> types = new List<Type>();
                AddInterface(types, type);
                return types;
            }

            return SelfAndBaseClasses(type);
        }

        private void AddSymbol(string name, object value)
        {
            if (this.symbols.ContainsKey(name))
            {
                throw this.ParseError(Resources.DuplicateIdentifier, name);
            }

            this.symbols.Add(name, value);
        }

        private void CheckAndPromoteOperand(Type signatures, string opName, ref Expression expr, int errorPos)
        {
            Expression[] args = new[] { expr };
            if (this.FindMethod(signatures, "F", false, args, out _) != 1)
            {
                throw this.ParseError(errorPos, Resources.IncompatibleOperand, opName, GetTypeName(args[0].Type));
            }

            expr = args[0];
        }

        private void CheckAndPromoteOperands(Type signatures, string opName, ref Expression left, ref Expression right, int errorPos)
        {
            Expression[] args = new[] { left, right };
            if (this.FindMethod(signatures, "F", false, args, out _) != 1)
            {
                throw this.IncompatibleOperandsError(opName, left, right, errorPos);
            }

            left = args[0];
            right = args[1];
        }

        private Expression CreateLiteral(object value, string text)
        {
            ConstantExpression expr = Expression.Constant(value);
            this.literals.Add(expr, text);
            return expr;
        }

        private int FindBestMethod(IEnumerable<MethodBase> methods, Expression[] args, out MethodBase method)
        {
            MethodData[] applicable = methods
                .Select(m => new MethodData
                {
                    MethodBase = m,
                    Parameters = m.GetParameters(),
                })
                .Where(m => this.IsApplicable(m, args))
                .ToArray();

            if (applicable.Length > 1)
            {
                applicable = applicable
                    .Where(m => applicable.All(n => m == n || IsBetterThan(args, m, n)))
                    .ToArray();
            }

            if (applicable.Length == 1)
            {
                MethodData md = applicable[0];
                for (int i = 0; i < args.Length; i++)
                {
                    args[i] = md.Args[i];
                }

                method = md.MethodBase;
            }
            else
            {
                method = null;
            }

            return applicable.Length;
        }

        private int FindIndexer(Type type, Expression[] args, out MethodBase method)
        {
            foreach (Type t in SelfAndBaseTypes(type))
            {
                MemberInfo[] members = t.GetDefaultMembers();
                if (members.Length != 0)
                {
                    IEnumerable<MethodBase> methods = members.OfType<PropertyInfo>()
                        .Select(p => (MethodBase)p.GetGetMethod())
                        .Where(m => m != null);

                    int count = this.FindBestMethod(methods, args, out method);
                    if (count != 0)
                    {
                        return count;
                    }
                }
            }

            method = null;
            return 0;
        }

        private int FindMethod(Type type, string methodName, bool staticAccess, Expression[] args, out MethodBase method)
        {
            BindingFlags flags = BindingFlags.Public | BindingFlags.DeclaredOnly |
                (staticAccess ? BindingFlags.Static : BindingFlags.Instance);
            foreach (Type t in SelfAndBaseTypes(type))
            {
                MemberInfo[] members = t.FindMembers(MemberTypes.Method, flags, Type.FilterNameIgnoreCase, methodName);
                int count = this.FindBestMethod(members.Cast<MethodBase>(), args, out method);
                if (count != 0)
                {
                    return count;
                }
            }

            method = null;
            return 0;
        }

        private MemberInfo FindPropertyOrField(Type type, string memberName, bool staticAccess)
        {
            BindingFlags flags = BindingFlags.Public | BindingFlags.DeclaredOnly | (staticAccess ? BindingFlags.Static : BindingFlags.Instance);
            foreach (Type t in SelfAndBaseTypes(type))
            {
                MemberInfo[] members = t.FindMembers(MemberTypes.Property | MemberTypes.Field, flags, Type.FilterNameIgnoreCase, memberName);
                if (members.Length != 0)
                {
                    return members[0];
                }
            }

            return null;
        }

        private Expression GenerateAdd(Expression left, Expression right)
        {
            if (left.Type == typeof(string) && right.Type == typeof(string))
            {
                return this.GenerateStaticMethodCall("Concat", left, right);
            }

            return Expression.Add(left, right);
        }

        private Expression GenerateConditional(Expression test, Expression expr1, Expression expr2, int errorPos)
        {
            if (test.Type != typeof(bool))
            {
                throw this.ParseError(errorPos, Resources.FirstExprMustBeBool);
            }

            if (expr1.Type != expr2.Type)
            {
                Expression expr1as2 = expr2 != NullLiteral ? this.PromoteExpression(expr1, expr2.Type, true) : null;
                Expression expr2as1 = expr1 != NullLiteral ? this.PromoteExpression(expr2, expr1.Type, true) : null;
                if (expr1as2 != null && expr2as1 == null)
                {
                    expr1 = expr1as2;
                }
                else if (expr2as1 != null && expr1as2 == null)
                {
                    expr2 = expr2as1;
                }
                else
                {
                    string type1 = expr1 != NullLiteral ? expr1.Type.Name : "null";
                    string type2 = expr2 != NullLiteral ? expr2.Type.Name : "null";

                    // Here  expr2as1 is not null
                    if (expr1as2 != null)
                    {
                        throw this.ParseError(errorPos, Resources.BothTypesConvertToOther, type1, type2);
                    }

                    throw this.ParseError(errorPos, Resources.NeitherTypeConvertsToOther, type1, type2);
                }
            }

            return Expression.Condition(test, expr1, expr2);
        }

        private Expression GenerateConversion(Expression expr, Type type, int errorPos)
        {
            Type exprType = expr.Type;
            if (exprType == type)
            {
                return expr;
            }

            if (exprType.IsValueType && type.IsValueType)
            {
                if ((IsNullableType(exprType) || IsNullableType(type)) && GetNonNullableType(exprType) == GetNonNullableType(type))
                {
                    return Expression.Convert(expr, type);
                }

                if ((IsNumericType(exprType) || IsEnumType(exprType)) && (IsNumericType(type) || IsEnumType(type)))
                {
                    return Expression.ConvertChecked(expr, type);
                }
            }

            if (exprType.IsAssignableFrom(type) || type.IsAssignableFrom(exprType) || exprType.IsInterface || type.IsInterface)
            {
                return Expression.Convert(expr, type);
            }

            throw this.ParseError(errorPos, Resources.CannotConvertValue, GetTypeName(exprType), GetTypeName(type));
        }

        private Expression GenerateEqual(Expression left, Expression right)
        {
            return Expression.Equal(left, right);
        }

        private Expression GenerateGreaterThan(Expression left, Expression right)
        {
            if (left.Type == typeof(string))
            {
                return Expression.GreaterThan(
                    this.GenerateStaticMethodCall("Compare", left, right),
                    Expression.Constant(0));
            }

            return Expression.GreaterThan(left, right);
        }

        private Expression GenerateGreaterThanEqual(Expression left, Expression right)
        {
            if (left.Type == typeof(string))
            {
                return Expression.GreaterThanOrEqual(
                    this.GenerateStaticMethodCall("Compare", left, right),
                    Expression.Constant(0));
            }

            return Expression.GreaterThanOrEqual(left, right);
        }

        private Expression GenerateLessThan(Expression left, Expression right)
        {
            if (left.Type == typeof(string))
            {
                return Expression.LessThan(
                    this.GenerateStaticMethodCall("Compare", left, right),
                    Expression.Constant(0));
            }

            return Expression.LessThan(left, right);
        }

        private Expression GenerateLessThanEqual(Expression left, Expression right)
        {
            if (left.Type == typeof(string))
            {
                return Expression.LessThanOrEqual(
                    this.GenerateStaticMethodCall("Compare", left, right),
                    Expression.Constant(0));
            }

            return Expression.LessThanOrEqual(left, right);
        }

        private Expression GenerateNotEqual(Expression left, Expression right)
        {
            return Expression.NotEqual(left, right);
        }

        private Expression GenerateStaticMethodCall(string methodName, Expression left, Expression right)
        {
            return Expression.Call(null, this.GetStaticMethod(methodName, left, right), new[] { left, right });
        }

        private Expression GenerateStringConcat(Expression left, Expression right)
        {
            return Expression.Call(
                null,
                typeof(string).GetMethod("Concat", new[] { typeof(object), typeof(object) }),
                new[] { left, right });
        }

        private Expression GenerateSubtract(Expression left, Expression right)
        {
            return Expression.Subtract(left, right);
        }

        private string GetIdentifier()
        {
            this.ValidateToken(TokenId.Identifier, Resources.IdentifierExpected);
            string id = this.token.Text;
            if (id.Length > 1 && id[0] == '@')
            {
                id = id.Substring(1);
            }

            return id;
        }

        private MethodInfo GetStaticMethod(string methodName, Expression left, Expression right)
        {
            return left.Type.GetMethod(methodName, new[] { left.Type, right.Type });
        }

        private Exception IncompatibleOperandsError(string opName, Expression left, Expression right, int pos)
        {
            return this.ParseError(pos, Resources.IncompatibleOperands, opName, GetTypeName(left.Type), GetTypeName(right.Type));
        }

        private bool IsApplicable(MethodData method, Expression[] args)
        {
            if (method.Parameters.Length != args.Length)
            {
                return false;
            }

            Expression[] promotedArgs = new Expression[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                ParameterInfo pi = method.Parameters[i];
                if (pi.IsOut)
                {
                    return false;
                }

                Expression promoted = this.PromoteExpression(args[i], pi.ParameterType, false);
                if (promoted == null)
                {
                    return false;
                }

                promotedArgs[i] = promoted;
            }

            method.Args = promotedArgs;
            return true;
        }

        private void NextChar()
        {
            if (this.textPos < this.textLen)
            {
                this.textPos++;
            }

            this.ch = this.textPos < this.textLen ? this.text[this.textPos] : '\0';
        }

        private void NextToken()
        {
            while (char.IsWhiteSpace(this.ch))
            {
                this.NextChar();
            }

            TokenId t;
            int tokenPos = this.textPos;
            switch (this.ch)
            {
                case '!':
                    this.NextChar();
                    if (this.ch == '=')
                    {
                        this.NextChar();
                        t = TokenId.ExclamationEqual;
                    }
                    else
                    {
                        t = TokenId.Exclamation;
                    }

                    break;

                case '%':
                    this.NextChar();
                    t = TokenId.Percent;

                    break;

                case '&':
                    this.NextChar();
                    if (this.ch == '&')
                    {
                        this.NextChar();
                        t = TokenId.DoubleAmphersand;
                    }
                    else
                    {
                        t = TokenId.Amphersand;
                    }

                    break;

                case '(':
                    this.NextChar();
                    t = TokenId.OpenParen;

                    break;

                case ')':
                    this.NextChar();
                    t = TokenId.CloseParen;

                    break;

                case '*':
                    this.NextChar();
                    t = TokenId.Asterisk;

                    break;

                case '+':
                    this.NextChar();
                    t = TokenId.Plus;

                    break;

                case ',':
                    this.NextChar();
                    t = TokenId.Comma;

                    break;

                case '-':
                    this.NextChar();
                    t = TokenId.Minus;

                    break;

                case '.':
                    this.NextChar();
                    t = TokenId.Dot;

                    break;

                case '/':
                    this.NextChar();
                    t = TokenId.Slash;

                    break;

                case ':':
                    this.NextChar();
                    t = TokenId.Colon;

                    break;

                case '<':
                    this.NextChar();
                    if (this.ch == '=')
                    {
                        this.NextChar();
                        t = TokenId.LessThanEqual;
                    }
                    else if (this.ch == '>')
                    {
                        this.NextChar();
                        t = TokenId.LessGreater;
                    }
                    else
                    {
                        t = TokenId.LessThan;
                    }

                    break;

                case '=':
                    this.NextChar();
                    if (this.ch == '=')
                    {
                        this.NextChar();
                        t = TokenId.DoubleEqual;
                    }
                    else
                    {
                        t = TokenId.Equal;
                    }

                    break;

                case '>':
                    this.NextChar();
                    if (this.ch == '=')
                    {
                        this.NextChar();
                        t = TokenId.GreaterThanEqual;
                    }
                    else
                    {
                        t = TokenId.GreaterThan;
                    }

                    break;

                case '?':
                    this.NextChar();
                    t = TokenId.Question;

                    break;

                case '[':
                    this.NextChar();
                    t = TokenId.OpenBracket;

                    break;

                case ']':
                    this.NextChar();
                    t = TokenId.CloseBracket;

                    break;

                case '|':
                    this.NextChar();
                    if (this.ch == '|')
                    {
                        this.NextChar();
                        t = TokenId.DoubleBar;
                    }
                    else
                    {
                        t = TokenId.Bar;
                    }

                    break;

                case '"':
                case '\'':
                    char quote = this.ch;
                    do
                    {
                        this.NextChar();
                        while (this.textPos < this.textLen && this.ch != quote)
                        {
                            this.NextChar();
                        }

                        if (this.textPos == this.textLen)
                        {
                            throw this.ParseError(this.textPos, Resources.UnterminatedStringLiteral);
                        }

                        this.NextChar();
                    }
                    while (this.ch == quote);
                    t = TokenId.StringLiteral;

                    break;

                default:
                    if (char.IsLetter(this.ch) || this.ch == '@' || this.ch == '_')
                    {
                        do
                        {
                            this.NextChar();
                        }
                        while (char.IsLetterOrDigit(this.ch) || this.ch == '_');
                        t = TokenId.Identifier;

                        break;
                    }

                    if (char.IsDigit(this.ch))
                    {
                        t = TokenId.IntegerLiteral;
                        do
                        {
                            this.NextChar();
                        }
                        while (char.IsDigit(this.ch));

                        if (this.ch == '.')
                        {
                            t = TokenId.RealLiteral;
                            this.NextChar();
                            this.ValidateDigit();
                            do
                            {
                                this.NextChar();
                            }
                            while (char.IsDigit(this.ch));
                        }

                        if (this.ch == 'E' || this.ch == 'e')
                        {
                            t = TokenId.RealLiteral;
                            this.NextChar();
                            if (this.ch == '+' || this.ch == '-')
                            {
                                this.NextChar();
                            }

                            this.ValidateDigit();
                            do
                            {
                                this.NextChar();
                            }
                            while (char.IsDigit(this.ch));
                        }

                        if (this.ch == 'F' || this.ch == 'f' || this.ch == 'L' || this.ch == 'l' || this.ch == 'M' || this.ch == 'm')
                        {
                            this.NextChar();
                        }

                        break;
                    }

                    if (this.textPos == this.textLen)
                    {
                        t = TokenId.End;
                        break;
                    }

                    throw this.ParseError(this.textPos, Resources.InvalidCharacter, this.ch);
            }

            this.token.Id = t;
            this.token.Text = this.text[tokenPos..this.textPos];
            this.token.Pos = tokenPos;
        }

        // +, -, & operators
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S907:\"goto\" statement should not be used", Justification = "Not Applicable")]
        private Expression ParseAdditive()
        {
            Expression left = this.ParseMultiplicative();
            while (
                this.token.Id == TokenId.Plus ||
                this.token.Id == TokenId.Minus ||
                this.token.Id == TokenId.Amphersand)
            {
                Token op = this.token;
                this.NextToken();
                Expression right = this.ParseMultiplicative();
                switch (op.Id)
                {
                    case TokenId.Plus:
                        if (left.Type == typeof(string) || right.Type == typeof(string))
                        {
                            goto case TokenId.Amphersand;
                        }

                        this.CheckAndPromoteOperands(typeof(IAddSignatures), op.Text, ref left, ref right, op.Pos);
                        left = this.GenerateAdd(left, right);
                        break;

                    case TokenId.Minus:
                        this.CheckAndPromoteOperands(typeof(ISubtractSignatures), op.Text, ref left, ref right, op.Pos);
                        left = this.GenerateSubtract(left, right);
                        break;

                    case TokenId.Amphersand:
                        left = this.GenerateStringConcat(left, right);
                        break;

                    default:
                        break;
                }
            }

            return left;
        }

        private Expression ParseAggregate(Expression instance, Type elementType, string methodName, int errorPos)
        {
            this.outerIt = this.it;
            ParameterExpression innerIt = Expression.Parameter(elementType, string.Empty);
            this.it = innerIt;
            Expression[] args = this.ParseArgumentList();
            this.it = this.outerIt;
            if (this.FindMethod(typeof(IEnumerableSignatures), methodName, false, args, out MethodBase signature) != 1)
            {
                throw this.ParseError(errorPos, Resources.NoApplicableAggregate, methodName);
            }

            Type[] typeArgs;
            if (signature.Name == "Min" || signature.Name == "Max")
            {
                typeArgs = new[] { elementType, args[0].Type };
            }
            else
            {
                typeArgs = new[] { elementType };
            }

            if (args.Length == 0)
            {
                args = new[] { instance };
            }
            else
            {
                if (signature.Name == "Contains")
                {
                    args = new[] { instance, args[0] };
                }
                else
                {
                    args = new[] { instance, Expression.Lambda(args[0], innerIt) };
                }
            }

            return Expression.Call(typeof(Enumerable), signature.Name, typeArgs, args);
        }

        private Expression[] ParseArgumentList()
        {
            this.ValidateToken(TokenId.OpenParen, Resources.OpenParenthesisExpected);
            this.NextToken();
            Expression[] args = this.token.Id != TokenId.CloseParen ? this.ParseArguments() : Array.Empty<Expression>();
            this.ValidateToken(TokenId.CloseParen, Resources.CloseParenthesisOrCommaExpected);
            this.NextToken();
            return args;
        }

        private Expression[] ParseArguments()
        {
            List<Expression> argList = new List<Expression>();
            while (true)
            {
                argList.Add(this.ParseExpression());
                if (this.token.Id != TokenId.Comma)
                {
                    break;
                }

                this.NextToken();
            }

            return argList.ToArray();
        }

        // =, ==, !=, <>, >, >=, <, <= operators
        private Expression ParseComparison()
        {
            Expression left = this.ParseAdditive();
            while (
                this.token.Id == TokenId.Equal ||
                this.token.Id == TokenId.DoubleEqual ||
                this.token.Id == TokenId.ExclamationEqual ||
                this.token.Id == TokenId.LessGreater ||
                this.token.Id == TokenId.GreaterThan ||
                this.token.Id == TokenId.GreaterThanEqual ||
                this.token.Id == TokenId.LessThan ||
                this.token.Id == TokenId.LessThanEqual)
            {
                Token op = this.token;
                this.NextToken();
                Expression right = this.ParseAdditive();

                bool isEquality =
                    op.Id == TokenId.Equal ||
                    op.Id == TokenId.DoubleEqual ||
                    op.Id == TokenId.ExclamationEqual ||
                    op.Id == TokenId.LessGreater;

                if (isEquality && !left.Type.IsValueType && !right.Type.IsValueType)
                {
                    if (left.Type != right.Type)
                    {
                        if (left.Type.IsAssignableFrom(right.Type))
                        {
                            right = Expression.Convert(right, left.Type);
                        }
                        else if (right.Type.IsAssignableFrom(left.Type))
                        {
                            left = Expression.Convert(left, right.Type);
                        }
                        else
                        {
                            throw this.IncompatibleOperandsError(op.Text, left, right, op.Pos);
                        }
                    }
                }
                else if (IsEnumType(left.Type) || IsEnumType(right.Type))
                {
                    if (left.Type == right.Type)
                    {
                        // Convert both enums to integer
                        Expression e;
                        if ((e = this.PromoteExpression(right, typeof(int), true)) != null)
                        {
                            right = e;
                        }

                        if ((e = this.PromoteExpression(left, typeof(int), true)) != null)
                        {
                            left = e;
                        }
                    }
                    else
                    {
                        Expression e;
                        if ((e = this.PromoteExpression(right, left.Type, true)) != null)
                        {
                            right = e;
                        }
                        else if ((e = this.PromoteExpression(left, right.Type, true)) != null)
                        {
                            left = e;
                        }
                        else
                        {
                            throw this.IncompatibleOperandsError(op.Text, left, right, op.Pos);
                        }
                    }
                }
                else
                {
                    this.CheckAndPromoteOperands(
                        isEquality ? typeof(IEqualitySignatures) : typeof(IRelationalSignatures),
                        op.Text,
                        ref left,
                        ref right,
                        op.Pos);
                }

                switch (op.Id)
                {
                    case TokenId.Equal:
                    case TokenId.DoubleEqual:
                        left = this.GenerateEqual(left, right);
                        break;

                    case TokenId.ExclamationEqual:
                    case TokenId.LessGreater:
                        left = this.GenerateNotEqual(left, right);
                        break;

                    case TokenId.GreaterThan:
                        left = this.GenerateGreaterThan(left, right);
                        break;

                    case TokenId.GreaterThanEqual:
                        left = this.GenerateGreaterThanEqual(left, right);
                        break;

                    case TokenId.LessThan:
                        left = this.GenerateLessThan(left, right);
                        break;

                    case TokenId.LessThanEqual:
                        left = this.GenerateLessThanEqual(left, right);
                        break;

                    default:
                        break;
                }
            }

            return left;
        }

        private Expression ParseElementAccess(Expression expr)
        {
            int errorPos = this.token.Pos;
            this.ValidateToken(TokenId.OpenBracket, Resources.OpenParenthesisExpected);
            this.NextToken();
            Expression[] args = this.ParseArguments();
            this.ValidateToken(TokenId.CloseBracket, Resources.CloseBracketOrCommaExpected);
            this.NextToken();
            if (expr.Type.IsArray)
            {
                if (expr.Type.GetArrayRank() != 1 || args.Length != 1)
                {
                    throw this.ParseError(errorPos, Resources.CannotIndexMultiDimArray);
                }

                Expression index = this.PromoteExpression(args[0], typeof(int), true);
                if (index == null)
                {
                    throw this.ParseError(errorPos, Resources.InvalidIndex);
                }

                return Expression.ArrayIndex(expr, index);
            }
            else
            {
                return this.FindIndexer(expr.Type, args, out MethodBase mb) switch
                {
                    0 => throw this.ParseError(errorPos, Resources.NoApplicableIndexer, GetTypeName(expr.Type)),
                    1 => Expression.Call(expr, (MethodInfo)mb, args),
                    _ => throw this.ParseError(errorPos, Resources.AmbiguousIndexerInvocation, GetTypeName(expr.Type)),
                };
            }
        }

        private Exception ParseError(string format, params object[] args)
        {
            return this.ParseError(this.token.Pos, format, args);
        }

        private Exception ParseError(int pos, string format, params object[] args)
        {
            return new ParseException(string.Format(System.Globalization.CultureInfo.CurrentCulture, format, args), pos);
        }

        // ?: operator
        private Expression ParseExpression()
        {
            int errorPos = this.token.Pos;
            Expression expression = this.ParseLogicalOr();
            if (this.token.Id == TokenId.Question)
            {
                this.NextToken();
                Expression expr1 = this.ParseExpression();
                this.ValidateToken(TokenId.Colon, Resources.ColonExpected);
                this.NextToken();
                Expression expr2 = this.ParseExpression();
                expression = this.GenerateConditional(expression, expr1, expr2, errorPos);
            }

            return expression;
        }

        private Expression ParseIdentifier()
        {
            this.ValidateToken(TokenId.Identifier);
            if (Keywords.TryGetValue(this.token.Text, out object value))
            {
                if (value is Type t)
                {
                    return this.ParseTypeAccess(t);
                }

                if (value == (object)KeywordIt)
                {
                    return this.ParseIt();
                }

                if (value == (object)KeywordOuterIt)
                {
                    return this.ParseOuterIt();
                }

                if (value == (object)KeywordIif)
                {
                    return this.ParseIif();
                }

                if (value == (object)KeywordNew)
                {
                    return this.ParseNew();
                }

                this.NextToken();

                return (Expression)value;
            }

            if (this.symbols.TryGetValue(this.token.Text, out value) || this.externals?.TryGetValue(this.token.Text, out value) == true)
            {
                if (value is Expression expr)
                {
                    if (expr is LambdaExpression lambda)
                    {
                        return this.ParseLambdaInvocation(lambda);
                    }
                }
                else
                {
                    expr = Expression.Constant(value);
                }

                this.NextToken();
                return expr;
            }

            if (this.it != null)
            {
                return this.ParseMemberAccess(null, this.it);
            }

            throw this.ParseError(Resources.UnknownIdentifier, this.token.Text);
        }

        private Expression ParseIif()
        {
            int errorPos = this.token.Pos;
            this.NextToken();
            Expression[] args = this.ParseArgumentList();
            if (args.Length != 3)
            {
                throw this.ParseError(errorPos, Resources.IifRequiresThreeArgs);
            }

            return this.GenerateConditional(args[0], args[1], args[2], errorPos);
        }

        private Expression ParseIntegerLiteral()
        {
            this.ValidateToken(TokenId.IntegerLiteral);

            string t = this.token.Text.TrimEnd(new[] { 'L', 'l' });
            if (t[0] != '-')
            {
                if (!ulong.TryParse(t, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out ulong value))
                {
                    throw this.ParseError(Resources.InvalidIntegerLiteral, t);
                }

                this.NextToken();
                if (value <= (ulong)int.MaxValue)
                {
                    return this.CreateLiteral((int)value, t);
                }

                if (value <= (ulong)uint.MaxValue)
                {
                    return this.CreateLiteral((uint)value, t);
                }

                if (value <= (ulong)long.MaxValue)
                {
                    return this.CreateLiteral((long)value, t);
                }

                return this.CreateLiteral(value, t);
            }
            else
            {
                if (!long.TryParse(t, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out long value))
                {
                    throw this.ParseError(Resources.InvalidIntegerLiteral, t);
                }

                this.NextToken();
                if (value >= int.MinValue && value <= int.MaxValue)
                {
                    return this.CreateLiteral((int)value, t);
                }

                return this.CreateLiteral(value, t);
            }
        }

        private Expression ParseIt()
        {
            if (this.it == null)
            {
                throw this.ParseError(Resources.NoItInScope);
            }

            this.NextToken();
            return this.it;
        }

        private Expression ParseLambdaInvocation(LambdaExpression lambda)
        {
            int errorPos = this.token.Pos;
            this.NextToken();
            Expression[] args = this.ParseArgumentList();
            if (this.FindMethod(lambda.Type, "Invoke", false, args, out _) != 1)
            {
                throw this.ParseError(errorPos, Resources.ArgsIncompatibleWithLambda);
            }

            return Expression.Invoke(lambda, args);
        }

        // &&, and operator
        private Expression ParseLogicalAnd()
        {
            Expression left = this.ParseComparison();
            while (this.token.Id == TokenId.DoubleAmphersand || this.TokenIdentifierIs("and"))
            {
                Token op = this.token;
                this.NextToken();
                Expression right = this.ParseComparison();
                this.CheckAndPromoteOperands(typeof(ILogicalSignatures), op.Text, ref left, ref right, op.Pos);
                left = Expression.AndAlso(left, right);
            }

            return left;
        }

        // ||, or operator
        private Expression ParseLogicalOr()
        {
            Expression left = this.ParseLogicalAnd();
            while (this.token.Id == TokenId.DoubleBar || this.TokenIdentifierIs("or"))
            {
                Token op = this.token;
                this.NextToken();
                Expression right = this.ParseLogicalAnd();
                this.CheckAndPromoteOperands(typeof(ILogicalSignatures), op.Text, ref left, ref right, op.Pos);
                left = Expression.OrElse(left, right);
            }

            return left;
        }

        private Expression ParseMemberAccess(Type type, Expression instance)
        {
            if (instance != null)
            {
                type = instance.Type;
            }

            int errorPos = this.token.Pos;
            string id = this.GetIdentifier();
            this.NextToken();
            if (this.token.Id == TokenId.OpenParen)
            {
                if (instance != null && type != typeof(string))
                {
                    Type enumerableType = FindGenericType(typeof(IEnumerable<>), type);
                    if (enumerableType != null)
                    {
                        Type elementType = enumerableType.GetGenericArguments()[0];
                        return this.ParseAggregate(instance, elementType, id, errorPos);
                    }
                }

                Expression[] args = this.ParseArgumentList();
                return this.FindMethod(type, id, instance == null, args, out MethodBase mb) switch
                {
                    0 => throw this.ParseError(errorPos, Resources.NoApplicableMethod, id, GetTypeName(type)),
                    1 => Expression.Call(instance, (MethodInfo)mb, args),
                    _ => throw this.ParseError(errorPos, Resources.AmbiguousMethodInvocation, id, GetTypeName(type)),
                };
            }
            else
            {
                MemberInfo member = this.FindPropertyOrField(type, id, instance == null);
                if (member == null)
                {
                    throw this.ParseError(errorPos, Resources.UnknownPropertyOrField, id, GetTypeName(type));
                }

                return member is PropertyInfo ? Expression.Property(instance, (PropertyInfo)member) : Expression.Field(instance, (FieldInfo)member);
            }
        }

        // *, /, %, mod operators
        private Expression ParseMultiplicative()
        {
            Expression left = this.ParseUnary();
            while (
                this.token.Id == TokenId.Asterisk ||
                this.token.Id == TokenId.Slash ||
                this.token.Id == TokenId.Percent ||
                this.TokenIdentifierIs("mod"))
            {
                Token op = this.token;
                this.NextToken();
                Expression right = this.ParseUnary();
                this.CheckAndPromoteOperands(typeof(IArithmeticSignatures), op.Text, ref left, ref right, op.Pos);
                switch (op.Id)
                {
                    case TokenId.Asterisk:
                        left = Expression.Multiply(left, right);
                        break;

                    case TokenId.Slash:
                        left = Expression.Divide(left, right);
                        break;

                    case TokenId.Percent:
                    case TokenId.Identifier:
                        left = Expression.Modulo(left, right);
                        break;

                    default:
                        break;
                }
            }

            return left;
        }

        private Expression ParseNew()
        {
            this.NextToken();
            this.ValidateToken(TokenId.OpenParen, Resources.OpenParenthesisExpected);
            this.NextToken();
            List<DynamicProperty> properties = new List<DynamicProperty>();
            List<Expression> expressions = new List<Expression>();
            while (true)
            {
                int exprPos = this.token.Pos;
                Expression expr = this.ParseExpression();
                string propName;
                if (this.TokenIdentifierIs("as"))
                {
                    this.NextToken();
                    propName = this.GetIdentifier();
                    this.NextToken();
                }
                else
                {
                    if (expr is MemberExpression me)
                    {
                        propName = me.Member.Name;
                    }
                    else
                    {
                        throw this.ParseError(exprPos, Resources.MissingAsClause);
                    }
                }

                expressions.Add(expr);
                properties.Add(new DynamicProperty(propName, expr.Type));
                if (this.token.Id != TokenId.Comma)
                {
                    break;
                }

                this.NextToken();
            }

            this.ValidateToken(TokenId.CloseParen, Resources.CloseParenthesisOrCommaExpected);
            this.NextToken();
            Type type = DynamicExpressionParser.CreateClass(properties);
            MemberBinding[] bindings = new MemberBinding[properties.Count];
            for (int i = 0; i < bindings.Length; i++)
            {
                bindings[i] = Expression.Bind(type.GetProperty(properties[i].Name), expressions[i]);
            }

            return Expression.MemberInit(Expression.New(type), bindings);
        }

        private Expression ParseOuterIt()
        {
            if (this.outerIt == null)
            {
                throw this.ParseError(Resources.NoItInScope);
            }

            this.NextToken();
            return this.outerIt;
        }

        private Expression ParseParenExpression()
        {
            this.ValidateToken(TokenId.OpenParen, Resources.OpenParenthesisExpected);
            this.NextToken();
            Expression e = this.ParseExpression();
            this.ValidateToken(TokenId.CloseParen, Resources.CloseParenthesisOrOperatorExpected);
            this.NextToken();
            return e;
        }

        private Expression ParsePrimary()
        {
            Expression expr = this.ParsePrimaryStart();
            while (true)
            {
                if (this.token.Id == TokenId.Dot)
                {
                    this.NextToken();
                    expr = this.ParseMemberAccess(null, expr);
                }
                else if (this.token.Id == TokenId.OpenBracket)
                {
                    expr = this.ParseElementAccess(expr);
                }
                else
                {
                    break;
                }
            }

            return expr;
        }

        private Expression ParsePrimaryStart()
        {
            return this.token.Id switch
            {
                TokenId.Identifier => this.ParseIdentifier(),
                TokenId.StringLiteral => this.ParseStringLiteral(),
                TokenId.IntegerLiteral => this.ParseIntegerLiteral(),
                TokenId.RealLiteral => this.ParseRealLiteral(),
                TokenId.OpenParen => this.ParseParenExpression(),
                _ => throw this.ParseError(Resources.ExpressionExpected),
            };
        }

        private Expression ParseRealLiteral()
        {
            this.ValidateToken(TokenId.RealLiteral);
            string t = this.token.Text;
            object value = null;
            char last = t[^1];

            switch (last)
            {
                case 'M':
                case 'm':
                    if (decimal.TryParse(t[0..^1], NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.InvariantInfo, out decimal dc))
                    {
                        value = dc;
                    }

                    break;

                case 'F':
                case 'f':
                    if (float.TryParse(t[0..^1], NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.InvariantInfo, out float fl))
                    {
                        value = fl;
                    }

                    break;

                default:
                    if (double.TryParse(t, NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.InvariantInfo, out double db))
                    {
                        value = db;
                    }

                    break;
            }

            if (value == null)
            {
                throw this.ParseError(Resources.InvalidRealLiteral, t);
            }

            this.NextToken();
            return this.CreateLiteral(value, t);
        }

        private Expression ParseStringLiteral()
        {
            this.ValidateToken(TokenId.StringLiteral);
            char quote = this.token.Text[0];
            string s = this.token.Text[1..^1];
            int start = 0;
            while (true)
            {
                int i = s.IndexOf(quote, start);
                if (i < 0)
                {
                    break;
                }

                s = s.Remove(i, 1);
                start = i + 1;
            }

            if (quote == '\'')
            {
                if (s.Length != 1)
                {
                    throw this.ParseError(Resources.InvalidCharacterLiteral);
                }

                this.NextToken();
                return this.CreateLiteral(s[0], s);
            }

            this.NextToken();
            return this.CreateLiteral(s, s);
        }

        private Expression ParseTypeAccess(Type type)
        {
            int errorPos = this.token.Pos;
            this.NextToken();
            if (this.token.Id == TokenId.Question)
            {
                if (!type.IsValueType || IsNullableType(type))
                {
                    throw this.ParseError(errorPos, Resources.TypeHasNoNullableForm, GetTypeName(type));
                }

                type = typeof(Nullable<>).MakeGenericType(type);
                this.NextToken();
            }

            if (this.token.Id == TokenId.OpenParen)
            {
                Expression[] args = this.ParseArgumentList();
                switch (this.FindBestMethod(type.GetConstructors(), args, out MethodBase method))
                {
                    case 0:
                        if (args.Length == 1)
                        {
                            return this.GenerateConversion(args[0], type, errorPos);
                        }

                        throw this.ParseError(errorPos, Resources.NoMatchingConstructor, GetTypeName(type));

                    case 1:
                        return Expression.New((ConstructorInfo)method, args);

                    default:
                        throw this.ParseError(errorPos, Resources.AmbiguousConstructorInvocation, GetTypeName(type));
                }
            }

            this.ValidateToken(TokenId.Dot, Resources.DotOrOpenParenthesisExpected);
            this.NextToken();
            return this.ParseMemberAccess(type, null);
        }

        // -, !, not unary operators
        private Expression ParseUnary()
        {
            if (
                this.token.Id == TokenId.Minus ||
                this.token.Id == TokenId.Exclamation ||
                this.TokenIdentifierIs("not"))
            {
                Token op = this.token;
                this.NextToken();
                if (op.Id == TokenId.Minus && (this.token.Id == TokenId.IntegerLiteral || this.token.Id == TokenId.RealLiteral))
                {
                    this.token.Text = "-" + this.token.Text;
                    this.token.Pos = op.Pos;
                    return this.ParsePrimary();
                }

                Expression expr = this.ParseUnary();
                if (op.Id == TokenId.Minus)
                {
                    this.CheckAndPromoteOperand(typeof(INegationSignatures), op.Text, ref expr, op.Pos);
                    expr = Expression.Negate(expr);
                }
                else
                {
                    this.CheckAndPromoteOperand(typeof(INotSignatures), op.Text, ref expr, op.Pos);
                    expr = Expression.Not(expr);
                }

                return expr;
            }

            return this.ParsePrimary();
        }

        private void ProcessParameters(ParameterExpression[] parameters)
        {
            foreach (ParameterExpression pe in parameters)
            {
                if (!string.IsNullOrEmpty(pe.Name))
                {
                    this.AddSymbol(pe.Name, pe);
                }
            }

            if (parameters.Length == 1 && string.IsNullOrEmpty(parameters[0].Name))
            {
                this.it = parameters[0];
            }
        }

        private void ProcessValues(object[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                object value = values[i];
                if (i == values.Length - 1 && value is IDictionary<string, object>)
                {
                    this.externals = (IDictionary<string, object>)value;
                }
                else
                {
                    this.AddSymbol("@" + i.ToString(System.Globalization.CultureInfo.InvariantCulture), value);
                }
            }
        }

        private Expression PromoteExpression(Expression expr, Type type, bool exact)
        {
            if (expr.Type == type)
            {
                return expr;
            }

            if (expr is ConstantExpression ce)
            {
                if (ce == NullLiteral)
                {
                    if (!type.IsValueType || IsNullableType(type))
                    {
                        return Expression.Constant(null, type);
                    }
                }
                else
                {
                    if (this.literals.TryGetValue(ce, out string t))
                    {
                        Type target = GetNonNullableType(type);
                        object value = null;
                        switch (Type.GetTypeCode(ce.Type))
                        {
                            case TypeCode.Int32:
                            case TypeCode.UInt32:
                            case TypeCode.Int64:
                            case TypeCode.UInt64:
                                value = ParseNumber(t, target);
                                break;

                            case TypeCode.Double:
                                if (target == typeof(decimal))
                                {
                                    value = ParseNumber(t, target);
                                }

                                break;

                            case TypeCode.String:
                                value = ParseEnum(t, target);
                                break;

                            default:
                                break;
                        }

                        if (value != null)
                        {
                            return Expression.Constant(value, type);
                        }
                    }
                }
            }

            if (IsCompatibleWith(expr.Type, type))
            {
                if (type.IsValueType || exact)
                {
                    return Expression.Convert(expr, type);
                }

                return expr;
            }

            return null;
        }

        private void SetTextPos(int pos)
        {
            this.textPos = pos;
            this.ch = this.textPos < this.textLen ? this.text[this.textPos] : '\0';
        }

        private bool TokenIdentifierIs(string id)
        {
            return this.token.Id == TokenId.Identifier && string.Equals(id, this.token.Text, StringComparison.OrdinalIgnoreCase);
        }

        private void ValidateDigit()
        {
            if (!char.IsDigit(this.ch))
            {
                throw this.ParseError(this.textPos, Resources.DigitExpected);
            }
        }

        private void ValidateToken(TokenId t, string errorMessage)
        {
            if (this.token.Id != t)
            {
                throw this.ParseError(errorMessage);
            }
        }

        private void ValidateToken(TokenId t)
        {
            if (this.token.Id != t)
            {
                throw this.ParseError(Resources.SyntaxError);
            }
        }

        private struct Token
        {
            public TokenId Id;
            public int Pos;
            public string Text;
        }

        private class MethodData
        {
            public Expression[] Args { get; set; }

            public MethodBase MethodBase { get; set; }

            public ParameterInfo[] Parameters { get; set; }
        }
    }
}

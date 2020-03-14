using System;

namespace Basin.Utils.CssToXpath {
    public class SelectorSyntaxException : Exception {
        public SelectorSyntaxException() { }
        public SelectorSyntaxException(string msg) : base(msg) { }
    }

    public class ExpressionException : Exception {
        public ExpressionException() { }
        public ExpressionException(string msg) : base(msg) { }
    }
}
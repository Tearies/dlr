using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Scripting.Debugging {

    #region Generated Exception Factory

    // *** BEGIN GENERATED CODE ***

    /// <summary>
    ///    Strongly-typed and parameterized string factory.
    /// </summary>

    internal static partial class ErrorStrings {
        internal static string JumpNotAllowedInNonLeafFrames {
            get {
                return ResourceManager.Default.GetResource("JumpNotAllowedInNonLeafFrames", "Frame location can only be changed in leaf frames");
            }
        }

        internal static string DebugContextAlreadyConnectedToTracePipeline {
            get {
                return ResourceManager.Default.GetResource("DebugContextAlreadyConnectedToTracePipeline", "Cannot create TracePipeline because DebugContext is already connected to another TracePipeline");
            }
        }

        internal static string InvalidSourceSpan {
            get {
                return ResourceManager.Default.GetResource("InvalidSourceSpan","Invalid SourceSpan");
            }
        }

        internal static string SetNextStatementOnlyAllowedInsideTraceback {
            get {
                return ResourceManager.Default.GetResource("SetNextStatementOnlyAllowedInsideTraceback", "Unable to perform SetNextStatement because current thread is not inside a traceback");
            }
        }

        internal static string ITracePipelineClosed {
            get {
                return ResourceManager.Default.GetResource("ITracePipelineClosed", "ITracePipeline cannot be used because it has been closed");
            }
        }

        internal static string InvalidFunctionVersion {
            get {
                return ResourceManager.Default.GetResource("InvalidFunctionVersion", "Frame cannot be remapped to function verion {0} because it does not exist");
            }
        }

        internal static string DebugInfoWithoutSymbolDocumentInfo {
            get {
                return ResourceManager.Default.GetResource("DebugInfoWithoutSymbolDocumentInfo", "Unable to transform LambdaExpression because DebugInfoExpression #{0} did not have a valid SymbolDocumentInfo");
            }
        }
    }

    #endregion
}

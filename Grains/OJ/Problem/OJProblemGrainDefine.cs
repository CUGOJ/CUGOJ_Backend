using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUGOJ.Grains.OJ.Problem
{
    public partial class OJProblemGrain
    {
        public enum OJProblemStatus
        {
            Online = 1,
            Ban,
            Offline,
            Delete
        }

        public enum OJSubmissionStatus
        {
            Accepted=1,
            Pending,
            Compiling,
            Judging,
            Presentation,
            WrongAnsswer,
            TimeLimitExceeded,
            MemoryLimitExceeded,
            StackMemoryLimitExceeded,
            OutputLimitExceeded,
            RuntimeError,
            CompileError,
            CompileOK,
            SystemError,
            PresentationError,
            UnknownError,
            PartiallyCorrect,
        }

        public class OJProblemProperties
        {

        }
    }
}

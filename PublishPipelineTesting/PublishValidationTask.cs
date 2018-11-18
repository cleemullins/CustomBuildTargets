using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Build.Framework;

namespace PublishPipelineTesting
{
    public class PublishValidationTask : Microsoft.Build.Utilities.Task
    {
        public override bool Execute()
        {
            Log.LogMessage(MessageImportance.High, "Hello World");            
            return false;
        }
    }
}

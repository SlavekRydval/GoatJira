namespace GoatJira.Helpers
{
    public static class EAUtils
    {
        /// <summary>
        /// returns true if a project is currently opened 
        /// </summary>
        /// <param name="Repository">the repository</param>
        /// <returns>true if a project is opened in EA</returns>
        public static bool IsProjectOpen(EA.Repository Repository)
        {
            try
            {
                EA.Collection c = Repository.Models;
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static string ReadTaggedValue(EA.Collection Collection, string TaggedValueName, string DefaultValue)
        {
            EA.TaggedValue tv = Collection.GetByName(TaggedValueName);
            return tv == null ? DefaultValue : tv.Value;
        }


        public static void WriteTaggedValue(EA.Collection Collection, string TaggedValueName, string Value)
        {
            EA.TaggedValue tv = Collection.GetByName(TaggedValueName);
            if (tv == null)
                tv = Collection.AddNew(TaggedValueName, "TaggedValue");
            tv.Value = Value;
            tv.Update();
        }

        /// <summary>
        /// Temporary function solving error in Sparx EA
        /// Can be called just only if there is opened a project
        /// </summary>
        /// <param name="Repository">Repository</param>
        /// <returns>ObjectType of context item</returns>
        public static EA.ObjectType emergencyGetContextItemType(EA.Repository Repository)
        {
            EA.ObjectType vOT = Repository.GetContextItemType();
            if (vOT == EA.ObjectType.otNone)
            {
                //try it to retype to Package and check if it is a model... 
                //if anything gets wrong, forget it
                try
                {
                    if (((EA.Package)(Repository.GetContextObject())).ParentID == 0)
                        return EA.ObjectType.otModel;

                    //string vGUID = ((EA.Package)(Repository.GetContextObject())).PackageGUID;
                    //foreach (EA.Package vPackage in Repository.Models)
                    //{
                    //    if (vPackage.PackageGUID == vGUID)
                    //        return EA.ObjectType.otModel;
                    //}
                }
                catch
                {
                    return vOT;
                }
            }
            return vOT;
        }

    }
}

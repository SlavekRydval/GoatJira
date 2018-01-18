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


        public static void WriteTaggedValue(EA.Collection Collection, string TaggedValueName, string Value, bool WriteValueToNotes = false)
        {
            EA.TaggedValue tv = Collection.GetByName(TaggedValueName);
            if (tv == null)
                tv = Collection.AddNew(TaggedValueName, "TaggedValue");

            if (WriteValueToNotes)
            {
                if (tv.Notes != Value)
                    tv.Notes = Value;
            }
            else
            {
                if (tv.Value != Value)
                    tv.Value = Value;
            }
            tv.Update();
        }

        public static void WriteTaggedValue(EA.Element element, string TaggedValueName, string Value, bool WriteValueToNotes = false)
            => WriteTaggedValue(element.TaggedValues, TaggedValueName, Value, WriteValueToNotes);



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

        /// <summary>
        /// Creates new package in ParentPackage. If ForceCreation is false and package with the same Name exists, this package is returned insted of creation of a new one.
        /// </summary>
        /// <param name="ParentPackage">Parent package where the new package should be created.</param>
        /// <param name="Name">Name of a new package.</param>
        /// <param name="ForceCreation">If true, new package is always created. 
        /// If false and package with the same Name in ParentPackage already exists, this package is returned insted of creation of a new one.</param>
        /// <returns>Required package</returns>
        public static EA.Package CreatePackage (EA.Package ParentPackage, string Name, bool ForceCreation)
        {
            if (!ForceCreation)
                foreach (EA.Package Subpackage in ParentPackage.Packages)
                    if (Subpackage.Name == Name)
                        return Subpackage;

            EA.Package result = ParentPackage.Packages.AddNew(Name, "Package");
            result.Update();
            ParentPackage.Packages.Refresh();
            return result;
        }













    }
}

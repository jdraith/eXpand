﻿using DevExpress.ExpressApp;
using DevExpress.Xpo;
using eXpand.ExpressApp.Core;
using eXpand.ExpressApp.WorldCreator.Core;
using eXpand.ExpressApp.WorldCreator.PersistentTypesHelpers;
using eXpand.Persistent.Base.General;
using eXpand.Persistent.Base.PersistentMetaData;
using System.Linq;

namespace eXpand.ExpressApp.WorldCreator.Observers
{
    public class PersistentReferenceMemberInfoObserver:ObjectObserver<IPersistentReferenceMemberInfo>
    {
        public PersistentReferenceMemberInfoObserver(ObjectSpace objectSpace) : base(objectSpace) {
        }
        protected override void OnSaving(ObjectManipulatingEventArgs<IPersistentReferenceMemberInfo> e)
        {
            base.OnSaving(e);
            if (e.Object.RelationType == RelationType.OneToMany&&e.Object.IsAssociation())
                createTheManyPart(e.Object);
        }

        void createTheManyPart(IPersistentReferenceMemberInfo persistentReferenceMemberInfo) {
            IPersistentClassInfo classInfo = PersistentClassInfoQuery.Find(ObjectSpace.Session, persistentReferenceMemberInfo.ReferenceTypeFullName);
            string collectionPropertyName = persistentReferenceMemberInfo.Name + "s";
            if (classInfo != null&&classInfo.OwnMembers.Where(info => info.Name==collectionPropertyName).FirstOrDefault()==null) {
                var associationAttribute =PersistentAttributeInfoQuery.Find<AssociationAttribute>(persistentReferenceMemberInfo);
                classInfo.CreateCollection(persistentReferenceMemberInfo.Owner.PersistentAssemblyInfo.Name ,
                                           persistentReferenceMemberInfo.Owner.Name).CreateAssociation(
                    associationAttribute.Name);
            }
        }
    }
}

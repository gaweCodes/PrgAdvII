using System;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Serialization;

namespace App {
    [DataContract]
    class CarSurrogate : IDataContractSurrogate {
        private string _Type;
        private int _Model;

        [DataMember]
        public string Type {
            get { return _Type; }
            set { _Type = value; }
        }

        [DataMember]
        public int Model {
            get { return _Model; }
            set { _Model = value; }
        }

        #region Implementations

        public Type GetDataContractType(Type type) {
            if (type == typeof(Car))
                return typeof(CarSurrogate);
            else
                return type;
        }

        public object GetObjectToSerialize(object obj, Type targetType) {
            if (obj is Car) {
                var car = (Car) obj;
                return new CarSurrogate {Model = car.Model, Type = car.Type};
            }

            return obj;
        }

        public object GetDeserializedObject(object obj, Type targetType) {
            if (obj is CarSurrogate) {
                var car = new Car();
                car.Model = ((CarSurrogate) obj).Model;
                car.Type = ((CarSurrogate) obj).Type;

                return car;
            }

            return obj;
        }


        #endregion

        #region NotImplemented

        public void GetKnownCustomDataTypes(Collection<Type> customDataTypes) {
            throw new NotSupportedException("unused");
        }

        public object GetCustomDataToExport(Type clrType, Type dataContractType) {
            throw new NotSupportedException("unused");
        }

        public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType) {
            throw new NotSupportedException("unused");
        }

        public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData) {
            throw new NotSupportedException("unused");
        }

        public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit) {
            throw new NotSupportedException("unused");
        }

        #endregion
    }
}

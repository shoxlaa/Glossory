using Glossory_MayBeFinal_.ViewModel;
using System;

namespace Glossory_MayBeFinal_.Services
{
    public class ViewModelFactory
    {
        private readonly IStoreDataBase _storeData; 


        public ViewModelFactory (IStoreDataBase storeDataBase)
        {
            _storeData = storeDataBase;
        }
        public IBaseViewModel Create(ViewModelType type)
        {
            return type switch
            {
                ViewModelType.StoreViewModel =>App.p_container.GetInstance<StoreViewModel>(),
                ViewModelType.AddViewModel => App.p_container.GetInstance<AddViewModel>(),


                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }
    }

}

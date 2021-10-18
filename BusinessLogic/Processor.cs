using DBModel;
using DBModel.Model;
using DomainModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Processor
    {
        private AEMContext aemContext = new AEMContext();
        public async Task<string> Login(LoginModel model)
        {
            //  var a = convertIsoToDateTime();
            //var returnData = await Shared.POSTCommand(UrlExtension.ApiUrl + "user/AddNewUser", model);
            var returnData = await Shared.POSTCommand(@"http://test-demo.aemenersol.com/api/Account/Login", model);
            Shared.Token = JsonConvert.DeserializeObject<string>(returnData); ;
            return returnData;
        }

        //http://test-demo.aemenersol.com/api/PlatformWell/GetPlatformWellActual
        public async Task<string> GetPlatformWellActual()
        {
            var returnData = await Shared.GetCommand(@"http://test-demo.aemenersol.com/api/PlatformWell/GetPlatformWellActual", Shared.Token);
            var list = JsonConvert.DeserializeObject<List<PlatformInfoModel>>(returnData);
            if (list != null && list.Count > 0)

            {
                // var isSuccess = JsonConvert.DeserializeObject<bool>(await Task.Run(() => SyncData(list)));
                var isSuccess = await Task.Run(() => SyncData(list));
            }
            return returnData;
        }

        public async Task<string> GetPlatformWellDummy()
        {
            var returnData = await Shared.GetCommand(@"http://test-demo.aemenersol.com/api/PlatformWell/GetPlatformWellDummy", Shared.Token);
            var list = JsonConvert.DeserializeObject<List<PlatformInfoModel>>(returnData);
            if (list != null && list.Count > 0)
            {
                await Task.Run(() => SyncData(list));
            }
            return returnData;
        }

        public bool SyncData(List<PlatformInfoModel> model)
        {
            try
            {
                var data = new List<PlatformInfo>();
                foreach (var item in model)
                {
                    var newItem = new PlatformInfo
                    {
                        Id = item.id,
                        Latitute = item.latitude,
                        Longitude = item.longitude,
                        PlatformName = item.uniqueName,
                        PlatformId = item.id,
                        UniqueName = item.uniqueName,
                        CreatedAt = ConvertDateTime(item.createdAt),
                        UpdatedAt = ConvertDateTime(item.updatedAt)
                    };

                    data.Add(newItem);
                    var platformName = newItem.UniqueName;

                    if (item.well != null && item.well.Count > 0)
                    {
                        foreach (var well in item.well)
                        {
                            var isUpdate = data.Where(x => x.Id == well.id).FirstOrDefault();
                            if (isUpdate != null)
                            {
                                isUpdate.Latitute = well.latitude;
                                isUpdate.Longitude = well.longitude;
                                isUpdate.PlatformId = well.platformId;
                                isUpdate.PlatformName = platformName;
                                isUpdate.UniqueName = well.uniqueName;
                                isUpdate.CreatedAt = ConvertDateTime(well.createdAt);
                                isUpdate.UpdatedAt = ConvertDateTime(well.updatedAt);

                            }
                            else
                            {
                                var newWell = new PlatformInfo
                                {
                                    Id = well.id,
                                    Latitute = well.latitude,
                                    Longitude = well.longitude,
                                    PlatformName = platformName,
                                    PlatformId = well.platformId,
                                    UniqueName = well.uniqueName,
                                    CreatedAt = ConvertDateTime(well.createdAt),
                                    UpdatedAt = ConvertDateTime(well.updatedAt)
                                };
                                data.Add(newWell);
                            }


                        }
                    }
                }

                //check if existing or not
                foreach (var item in data.ToList())
                {
                    var isExist = aemContext.PlatformInfo.Where(x => x.Id == item.Id).FirstOrDefault();
                    if(isExist != null)
                    {
                        data.Remove(item);
                        isExist.CreatedAt = item.CreatedAt;
                        isExist.Latitute = item.Latitute;
                        isExist.Longitude = item.Longitude;
                        isExist.PlatformId = item.PlatformId;
                        isExist.PlatformName = item.PlatformName;
                        isExist.UniqueName = item.UniqueName;
                        isExist.UpdatedAt = item.UpdatedAt;
                    }
                }

                
                aemContext.AddRange(data);

                return aemContext.SaveChanges() > 0;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        public DateTime? ConvertDateTime(string datetime)
        {
            if (!string.IsNullOrEmpty(datetime))
                return DateTime.ParseExact(datetime.Substring(0, 10) + datetime.Substring(11, 8), "yyyy-MM-ddHH:mm:ss", CultureInfo.InvariantCulture);
            else
                return null;
        }
    }
}

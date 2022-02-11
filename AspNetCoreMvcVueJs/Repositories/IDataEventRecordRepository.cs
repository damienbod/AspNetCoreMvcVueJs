using System.Collections.Generic;
using AspNetCoreMvcVueJs.Model;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvcVueJs.Repositories;

public interface IDataEventRecordRepository
{
    void Delete(long id);
    DataEventRecord Get(long id);
    List<DataEventRecord> GetAll();
    void Post(DataEventRecord dataEventRecord);
    void Put(long id, [FromBody] DataEventRecord dataEventRecord);
}
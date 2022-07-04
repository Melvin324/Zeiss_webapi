using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Zeiss_webapi.Models;

namespace Zeiss_webapi.Providers {
    public class MsgProvider {
        private static string _tableName = "Msg";
        private MyDbContext _context;

        public MsgProvider(MyDbContext context)
        {
            _context = context;
        }

        public bool Add(MsgEntity entity)
        {
            _context.Msg.Add(entity);
            _context.SaveChanges();
            return true;
        }

        public bool AddAsync(MsgEntity entity)
        {
            using (var context = new MyDbContext()){
                context.Msg.Add(entity);
                context.SaveChanges();
            }
            return true;
        }

        public DataResponse<MsgEntity> Query(MsgRequest request)
        {
            var response = new DataResponse<MsgEntity>() {Total = 0, Data = null};
            var sql = $"SELECT * FROM {_tableName} WHERE 1=1 ";
            if (!string.IsNullOrEmpty(request.Id)){
                sql += $"and id='{request.Id}'";
            }
            if (!string.IsNullOrEmpty(request.MachineId)){
                sql += $"and machineId='{request.MachineId}'";
            }
            if (!string.IsNullOrEmpty(request.Timestamp)){
                sql += $"and timestamp='{request.Timestamp}'";
            }
            if (!string.IsNullOrEmpty(request.Status)){
                sql += $"and status='{request.Status}'";
            }
            if (!string.IsNullOrEmpty(request.Topic)){
                sql += $"and topic='{request.Topic}'";
            }
            if (!string.IsNullOrEmpty(request.Event)){
                sql += $"and event='{request.Event}'";
            }
            if (request.PageIndex <= 0){
                request.PageIndex = 1;
            }
            if (request.PageSize <= 0){
                request.PageSize = 10;
            }
            var cnt = _context.Msg.FromSqlRaw(sql).Count();
            var result = _context.Msg.FromSqlRaw(sql)?.OrderByDescending(x => x.Timestamp)
                .Skip(request.PageSize * (request.PageIndex - 1)).Take(request.PageSize)?.ToList();
            response.Data = result?.ToList() ?? new List<MsgEntity>();
            response.Total = (long)cnt;
            return response;
        }
    }
}

﻿

using API.Data;
using API.Dto.request;
using API.Dto.response;


namespace API.Model
{
    public interface ICTPhieuTraRepository
    {
        Task<IEnumerable<ChiTietPhieuTra>> LayCTPTTheoMaPT(int maPT);
    }
}

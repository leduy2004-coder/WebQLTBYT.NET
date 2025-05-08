
using Microsoft.AspNetCore.Mvc;
using WEB.Api;
using WEB.Models;
using WEB.Models.Response;


namespace WEB.Controllers;

public class PhieuMuonController : Controller
{

    private readonly PhieuMuonService phieuMuonService;

    public PhieuMuonController(PhieuMuonService phieuMuonService)
    {
        this.phieuMuonService = phieuMuonService;
    }

   


}
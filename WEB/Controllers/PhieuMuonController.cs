
using Microsoft.AspNetCore.Mvc;
using WEB.Api;
using WEB.Models;
using WEB.Models.Response;


namespace WEB.Controllers;

public class PhieuMuonController : Controller
{

    private readonly PhieuMuonService phieuMuonService;
    private readonly ThietBiService thietBiService;
    public PhieuMuonController(PhieuMuonService phieuMuonService, ThietBiService thietBiService)
    {
        this.phieuMuonService = phieuMuonService;
        this.thietBiService = thietBiService;
    }

    
}
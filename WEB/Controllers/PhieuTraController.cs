
using Microsoft.AspNetCore.Mvc;
using WEB.Api;
using WEB.Models;
using WEB.Models.Response;


namespace WEB.Controllers;

public class PhieuTraController : Controller
{

    private readonly PhieuTraService phieuTraService;

    public PhieuTraController(PhieuTraService phieuTraService)
    {
        this.phieuTraService = phieuTraService;
    }

   


}
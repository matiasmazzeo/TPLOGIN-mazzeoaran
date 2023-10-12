﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP_Login.Models;
namespace TP_Login.Controllers;

public class Account : Controller
{
    private readonly ILogger<Account> _logger;

    public Account(ILogger<Account> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult LogIn()
    {
        return View();
    }
    [HttpPost]
    public IActionResult VerificarLogIn(string UserName, string Contrasena)
    {
        ViewBag.MensajeErrorLogIn = null;
        ViewBag.User = BD.TraerUsuario(UserName, "UserName");
        if (ViewBag.User != null && Contrasena == ViewBag.User.Contrasena)
        {
            return View("Bienvenida");
        }
        ViewBag.MensajeErrorLogIn = "Usuario y/o Contraseña no válidos";
        return View("LogIn");
    }

    public IActionResult SignIn()
    {
        return View();
    }
    [HttpPost]
    public IActionResult VerificarSignIn(string UserName, string Contrasena, string Nombre, string Email, int Telefono)
    {
        ViewBag.MensajeError = BD.AgregarUsuario(UserName, Contrasena, Nombre, Email, Telefono);
        if (ViewBag.MensajeError == null)
        {
            return View("LogIn");
        }
        return View("SignIn");
    }

    public IActionResult RecuperarContrasena()
    {
        return View();
    }


    public IActionResult VerificarRecuperarContrasena(string Email)
    {
        ViewBag.MensajeError = null;
        USUARIO user = BD.TraerUsuario(Email, "Email");
        ViewBag.user = user;
        if (ViewBag.user != null)
        {
            return View("CambiarContrasena");
        }
        ViewBag.MensajeError = "El Email ingresado no se encuentra registrado";
        return View("RecuperarContrasena");
    }

    [HttpPost]
    public IActionResult CambiarContrasena(string nuevaContrasena, string UserName)
    {
        BD.UpdateContrasena(UserName, nuevaContrasena);
        return View("LogIn");
    }
}
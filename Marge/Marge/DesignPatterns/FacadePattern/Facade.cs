﻿using Marge.DesignPatterns.AbstractFactoryPattern;
using Marge.DesignPatterns.FactoryPattern;
using Marge.DesignPatterns.ProxyPattern;
using Marge.Domain;
using Marge.GameObjects;
using Marge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.FacadePattern
{
    public class Facade
    {
        private AbstractFactory _paintBomb;
        private AbstractFactory _freeze;
        private Debuff _blackSplash;
        private Debuff _freezeYourself;
        private Buff _colorSplash;
        private Buff _freezeOthers;
        ConnectionProxy _chatService;
        private FactoryPattern.Factory _factory;


        public Facade(ConnectionProxy connectionProxy)
        {
            _chatService = connectionProxy;
            _paintBomb = new PaintBombFactory();
            _freeze = new FreezeFactory();
            _blackSplash = new BlackSplash(_chatService);
            _freezeYourself = new FreezeYourself(_chatService);
            _colorSplash = new ColorSplash(_chatService);
            _freezeOthers = new FreezeOthers(_chatService);
            _factory = new BonusFactory();

        }

        //Abstracy factory methods
        public void CreateBuff(TileType buffType)
        {

            if (buffType == TileType.BuffFreezeOthers)
            {
                _freeze.CreateBuff(_chatService).SendBuff();
            }


            if (buffType == TileType.BuffColorSplash)
            {
                // pakeisti abstraktu pavadinima sendfreeze i relatible
                _paintBomb.CreateBuff(_chatService).SendBuff();
            }
        }

        public void CreateDeBuff(TileType debuffType)
        {

            if (debuffType == TileType.DebuffFreezeYourself)
            {
                _freeze.CreateDebuff(_chatService).SendDebuff();
            }


            if (debuffType == TileType.DebuffBlackSplash)
            {
                // pakeisti abstraktu pavadinima sendfreeze i relatible
                _paintBomb.CreateDebuff(_chatService).SendDebuff();
            }
        }

        //Factory method

        public void CreateBonus(int bonusType)
        {
            _factory.CreateBonus(bonusType, _chatService).SendBonus();
        }



    }
}

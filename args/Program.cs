#pragma warning disable 642

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Globalization;
using Args;
using Logger;

///
/// ������������ ���� ��������  ����-���� ��� ������������ ���������� args.
/// 
///


namespace test
{

/// \file  Program.cs

/// \brief ����� �������� ���������� ���������� ��� ������, ���� ���� Main � ������� ������ ���������
   
/// ���������� ������������� ������������� ���������� ������� 
/// ��������� ������. ���������� ������� �������������� LOGGER.
/// ���� ����� ���� ����� - ���������� (��� �� ���������� ������), 
/// ������������, ����� � ��������.
/// ���������� ���, ��� ���������� � ������ ������� 
/// ��������� ���� ������� ��������, ��� �������� ���������� ����� -l.
/// ���� ����� -p ��������� �������� 0.5, �� �� ����� ���������� � 0.1.
/// ���� �� ������ -t1 � -t2 �������� ������������. ��� ����������
/// � ����� mkVHelp ������ Arg �� ������ ���������.
/// 

    class Program    {
        static public ArgFlg  hlpF ;  ///<[����](@ref Args.ArgFlg) ���������.
        static public ArgFlg  dbgF ;  ///<���� �������
        static public ArgFlg  vF ;    ///<���� ��� ������������ �������������� � ����������� �����
        static public ArgIntMM    logLvl ;  ///< ���� ������ ����
        static public ArgFloatMM  perCent ;  ///<   ���� ���������� ����
        static public ArgStr  flNm ;         ///<  ���� ���� ������
        static public ArgFlg  tsk1 ;         ///<  ������ ����� ��� ������ ������1.
        static public ArgFlg  tsk2 ;         ///<  ������ ����� ��� ������ ������2,
                                             ///< ���� �� ������ ������ ���� � ��������� ������.

        static  Program()       ///< ����������� ����������� ��� ���������� ����������
        {
           hlpF   =  new ArgFlg(false, "?","help",    "to see this help");
           vF     =  new ArgFlg(false, "v",  "verbose", "additional info");
           dbgF   =  new ArgFlg(false, "d",  "debug",   "debug mode");
           tsk1   =  new ArgFlg(false, "t1",  "workOne",   "to do some work");
           tsk1.show = false;
           tsk2   =  new ArgFlg(false, "t2",  "workNext",   "to do another work");
           tsk2.show = false;
         //  dbgF.required = true;                
           logLvl =  new ArgIntMM(1,      "l",  "log",   "log level", "LLL");
           logLvl.setMin(1);
           logLvl.setMax(8);

           //logLvl.show = false;
           flNm   =  new ArgStr("somefile.dat", "f", "file", "data file", "FLNM");
           flNm.required = true;
//           flNm.show = false;

           perCent   =  new ArgFloatMM(0.05, "p", "percent", "percent for something",  "PPP");
           perCent.setMax(100.0);
        }

        static public void version (out int major, out int minor, out int build){
             System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly(); 
             System.Version ver = asm.GetName().Version;
             major =  ver.Major;
             minor =  ver.Minor;
             build =  ver.Build;
        }

/** 

 �������� ������ ��������� � ���������� ������ ����������.
 � ��������� ������ �������� ������ ������ ���� �� ������ -t1 ��� -t2,
 ������� � ����������  tsk1 � tsk2 ������ �������� �� ����������,
 � ��� ���������� �� ������ ��������� ������� mkVHelp.



        
*/        
        
        static public  void usage(){

           Args.Arg.mkVHelp("to test command line arguments"
                                     ,  "-t1 | -t2", vF

                ,hlpF
                ,dbgF
                ,vF
                ,logLvl
                ,perCent
                ,flNm
                ,tsk1
                ,tsk2
                );
                Environment.Exit(1);
        }

/** 

  ������ ���� ����� ��� ��������� ������ '-v' � '-?' �����������
 � ������ ������� ������ `test.usage()`, ������� 
  � ����������� ����� ������ ������� ��������� ����� 

-----

     to test command line arguments (ver:2.1.0)
     usage:
      app [-?] [-d] [-v] [-l LLL] [-p PPP] -f FLNM  -t1 | -t
      options:
        -?          		: to see this help: True
        -d          		: debug mode: False
        -v          		: additional info: True
        -l LLL		: log level (1..8): 1
        -p PPP		: percent for something (..100): 0.05
       -f FLNM		: data file: somefile.dat
       -t1          		: to do some work: False
       -t2          		: to do another work: False
     '?' means the same as 'help'
     'd' means the same as 'debug'
     'v' means the same as 'verbose'
     'l' means the same as 'log'
     'p' means the same as 'percent'
     'f' means the same as 'file'
     't1' means the same as 'workOne'
     't2' means the same as 'workNext'

-----
        
*/

        [STAThread]
        static public void Main(string[] args)
        {

           for (int i = 0; i<args.Length; i++){
             if (hlpF.check(ref i, args))
               usage();
             else if (dbgF.check(ref i, args))
               ;
             else if (vF.check(ref i, args))
               ;
             else if (logLvl.check(ref i, args))
               ;
             else if (perCent.check(ref i, args))
               ;
             else if (flNm.check(ref i, args))
               ;
             else if (tsk1.check(ref i, args))
               ;
             else if (tsk2.check(ref i, args))
               ;
           }
           if (!tsk1 && !tsk2)
             usage();

           DateTime st = DateTime.Now;
           using (LOGGER Logger = new LOGGER(LOGGER.uitoLvl(logLvl))){
  						if (vF)
  						   Logger.cnslLvl = IMPORTANCELEVEL.Stats;

              for (uint i = 0; i < 9; i++)
              {
                  Logger.WriteLine(LOGGER.uitoLvl(i)
                      ,"this is {0} iteration {1}  "
                      , i, LOGGER.uitoLvl(i)
                  );
                  Thread.Sleep(500);
              }

              if (perCent == 0.5)  {
                perCent.set(0.1);  
                Logger.WriteLine( IMPORTANCELEVEL.Info
                    ,"...0.5 has been changed to 0.1..."
                    ,  (double)  perCent
                );
              }

              DateTime fn = DateTime.Now;
              Logger.WriteLine(IMPORTANCELEVEL.Stats, "time of work with file '{1}' is {0} secs"
                   , (fn - st).TotalSeconds, (string)flNm);

           }
        }
    }                
}

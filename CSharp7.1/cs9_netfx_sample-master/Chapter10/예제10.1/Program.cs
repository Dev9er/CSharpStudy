﻿
/* ================= 10.2 비동기 호출 ================= */

using System;
using System.IO;
using System.Text;

class FileState
{
    public byte[] Buffer;
    public FileStream File;
}

class Program
{
    static void Main(string[] args)
    {
        AsyncRead();
    }

    private static void AsyncRead()
    {
        FileStream fs = new FileStream(@"C:\windows\system32\drivers\etc\HOSTS", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

        FileState state = new FileState();
        state.Buffer = new byte[fs.Length];
        state.File = fs;

        fs.BeginRead(state.Buffer, 0, state.Buffer.Length, readCompleted, state);
        // Read가 완료된 후의 코드를 readCompleted로 넘겨서 처리

        Console.ReadLine();
        fs.Close();
    }

    // 읽기 작업이 완료되면 스레드 풀의 자유 스레드가 readCompleted 메서드를 실행
    static void readCompleted(IAsyncResult ar)
    {
        FileState state = ar.AsyncState as FileState;
        state.File.EndRead(ar);
        string txt = Encoding.UTF8.GetString(state.Buffer);
        Console.WriteLine(txt);
    }
}
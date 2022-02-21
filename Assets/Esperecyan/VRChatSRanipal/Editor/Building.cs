using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;

internal class Building
{
    /// <summary>
    /// ビルド先へREADMEを追加します。
    /// </summary>
    [PostProcessBuild]
    private static void AddReadmeFile(BuildTarget _, string pathToBuiltProject)
    {
        File.WriteAllText(
            Path.Combine(Path.GetDirectoryName(pathToBuiltProject), "README.txt"),
            @"VIVE Pro Eye のトラッキング情報を、OSCの9000番ポートへ送信します。

詳細は以下をご覧ください。
https://pokemori.booth.pm/items/3667516



本ツールは以下のライブラリを含みます。
--------------------------------------------------------------------------------
OscCore
https://github.com/vrchat/OscCore

MIT License

Copyright (c) 2019 Stella Cannefax

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the ""Software""), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
--------------------------------------------------------------------------------
VIVE Eye and Facial Tracking SDK
https://developer-express.vive.com/resources/vive-sense/eye-and-facial-tracking-sdk/download/latest/
https://developer-express.vive.com/resources/downloads/licenses-and-agreements/english/
--------------------------------------------------------------------------------
"
        );
    }
}

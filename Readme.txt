FileCollector

概要:
  特定の場所から指定したファイルを収集します。
  コピー元は、Windows ディレクトリとウェブサイトが指定できます。

使い方:
  Source:
    コピー元を指定します。
  Recursive:
    有効にすると、再帰的にファイルを検索します。
  Filter:
    ファイル名に含まれる文字列を指定します。
    大文字と小文字は区別されます。
  Reg. Expression:
    Filter に指定した文字列を正規表現として解釈します。
  Destination:
    コピー先を指定します。
    エクスプローラからディレクトリをドラッグ&ドロップして入力する事もできます。
  Directory Tree:
    コピー元のディレクトリ ツリーを維持します。
    但し、Source にウェブサイトを指定した場合は、無視されます。
  Collect:
    コピーを開始します。
    コピーが開始すると、ボタン テキストが Abort に変化します。
    Abort をクリックすると、コピーが中断されます。

コマンドライン オプション:
  /d:[Destination]
    Destination を指定します。
  /e
    Reg. Expression を有効にします。
  /f:[Filter]
    Filter を指定します。
  /n:[Encoding]
    Source が URL の場合、ウェブページの文字エンコーディングを指定します。
    (例) UTF-8 の場合
      FileCollector.exe /n:utf-8
  /r
    Recursive を有効にします。
  /s:[Source]
    Source を指定します。
  /t
    Directory Tree を有効にします。

  空白を含んだ文字列を指定する場合、オプションを"(ダブルクォーテーション)で囲みます。
    (例) FileCollector.exe "/s:C:\Program Files\FileCollector"

VRChatSRanipal
==============
VIVE Pro Eye (SRanipal/SR_Runtime) のまばたきトラッキングを、OSCの9000番ポートへ以下のパラメータとして送信します。
- /avatar/parameters/LeftEye
- /avatar/parameters/RightEye

[VRChatベータ版のOSC機能](https://docs.vrchat.com/v2022.1.1/docs/osc-overview) を利用してこれを受け取り、アバターへ反映することができます。

VRCExpressionParametersで、「LeftEye」「RightEye」という名前のパラメータを設定しておくことで、パラメータ値へ反映されます。

開発
====
クローン後、VIVE Eye and Facial Tracking SDKをインポートします。  
https://developer-express.vive.com/resources/vive-sense/eye-and-facial-tracking-sdk/download/latest/

ライセンス
==========
外部ライブラリを除く本リポジトリ内のソースコードはMPL-2.0です。

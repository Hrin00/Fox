# Fox

横板过关游戏  
横スクロールアクションゲーム  
Side scrolling action game  
紹介動画: [https://www.youtube.com/watch?v=VWwDGxdh16k](https://www.youtube.com/watch?v=4N_-Rcjt4_0)    

Unity Version 2021.3.19f1 在此版本Unity下会报错，但无视错误可正常运行  
Unity Version 2021.3.19f1 でエラがあるが、エラを無視して実行可能  
Unity Version 2021.3.19f1 have Errors but can run normally

2023/6/28  
语言只有中文，近期准备追加日语和英语  
言語は中国語のみ、日本語と英語バージョンは近日中に追加予定  
Only Chinese , going to add Japanese and English version in the near future  


2023/6/29  
实装了localization功能，并且初步将3种语言文本配置完成-->中文为本人(母语者)撰写/日语为研究室成员(母语者)撰写/英语为DeepL翻译后本人润色  
ローカライズ機能を実装した、テキストを3ヵ国語に初期配置完了-->中国語は本人から(母语者)/日本語はラボメンバーから(母语者)/英語はDeepLが翻訳して修正した  
The localization function was installed and the initial configuration of the text in 3 languages was completed --> Chinese by myself (native speaker) / Japanese by a member of the research lab (native speaker) / English translated by DeepL and then retouched by myself  

2023/6/30  
紹介動画アップロード
Localizationバグ修正  

2023/7/1  
WEBGLで出力する際の文字化けバグ修正、Localization(MainKey)バグ修正、Game Overテキスト修正  

残ったバグ：Localizationを使う際にGetTable関数でWaitForCompletion()が呼び出された、WEB Requestの時Addressablesが用いられる  
WEBGLはシングルスレッドのでAddressablesは使えないため、言語変更はダブルクリックが必要(ロード待ち？)、また、UnityRoomでAddressablesの問題もあるので、UnityRoomでうまく実行できない  

2023/7/1  
紹介動画修正  


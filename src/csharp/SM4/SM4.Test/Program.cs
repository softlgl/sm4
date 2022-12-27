using Sm4;

string data = @"我和我的祖国
一刻也不能分割
无论我走到哪里
都流出一首赞歌
我歌唱每一座高山
我歌唱每一条河
袅袅炊烟 小小村落
路上一道辙
我最亲爱的祖国
我永远紧依着你的心窝
你用你那母亲的脉搏
和我诉说
我的祖国和我
像海和浪花一朵
浪是那海的赤子
海是那浪的依托
每当大海在微笑
我就是笑的旋涡
我分担着海的忧愁
分享海的欢乐
我最亲爱的祖国
你是大海永不干涸
永远给我碧浪清波
心中的歌";

string key = "98145489617106616498";
string encryptECB = Sm4Crypto.EncryptECB(new Sm4Crypto { Key = key, Data = data });
Console.WriteLine("EncryptECB:{0}", encryptECB);
string decryptECB = Sm4Crypto.DecryptECB(new Sm4Crypto { Key = key, Data = encryptECB });
Console.WriteLine("DecryptECB:{0}", decryptECB);

string hexKey = "ceshi20221212";
string encryptECB2 = Sm4Crypto.EncryptECB(new Sm4Crypto { Key = hexKey, Data = data, HexString = true });
Console.WriteLine("EncryptECB2:{0}", encryptECB2);

var javaResult = "e30f82bc43343b1e156f1f7e20297fe37e6393bfa5dade0b2bfad573cece20b503db918ae6d39c0ad29a655c18e940873a07b0816dc5f2e59f80c8a3da77eba7d3a9bb9a10b6cea1b5467330b89b34b39856a9ea6cb2c988a9ab53c6aee07a596e57b415e8b885d7cdd17e668f2d4f907a83cc88c79aadb47747f0e869e03f13dd8117c31c2a1dd6d882ca511de2c9230ce7fb6c59a208a12fcfa133250feb739efaa462ea1ad32cd25e15b55d54affdbe7e4dbb52663f2604a717bc188da8cf65dc77d8d88faf39a1153115e994bfa0dda5ab9c7b311b25285b594fe05f5b9a87dbef298d66525e1fd48660d6bc54200a0749e90bf4716477fc571dfb1485989d84806d00b308adf9cbb08263091d1072ee5a7cb4d790ce350a3b976d5c626877a78a537d62b464cd7704bed9d9c09882bf4140c84a4eeb6a5a539704e3e0b75ba53cc48e280a41c585cc68e567ac8b71c051ab40d042281b42b93903214e43ce26906c7810b43b95d4698fbd86ebff692bdb637465557da34ab2e688e83933ceec75c7111a4be1f86009202481586263e822794172e983221f643a57c4f472dc1dff201893a60480e3b8671a03cbe9bd4bb6148d30764424ac17379b443fa36c9996aceca55b58be12d64af326889d9a0d796a70f292ff1f337636b5a3a2898ad77aaf9fd6c3c2a06042f5721666b00c5f137dbaae0e5e3839cd204e1bf32c1f32c7e2344e29d1aab9c0d88924c784a2f3412eb9e972a8a8eaa27f15d8ad38d9014af21dfe63b1fb217290cada4f94";
string decryptECB2 = Sm4Crypto.DecryptECB(new Sm4Crypto { Key = hexKey, Data = javaResult, HexString = true });
Console.WriteLine("DecryptECB2:{0}", decryptECB2);

Console.ReadLine();
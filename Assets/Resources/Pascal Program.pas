begin
  var txt := readalllines('Paronyms.txt',encoding.UTF8);
  for var i := 0 to txt.Length-1 do
    while txt[i].Contains(char.ConvertFromUtf32(13)) do
      begin
      txt[i].Remove(txt[i].IndexOf(chr(13)));
Print(11);
end;
    var f := OpenWrite('Paronyms.txt',encoding.UTF8);
    foreach var st in txt do
      f.Writeln(st);
    f.Close;
end.
dotnet publish AgariTaku/AgariTaku.csproj -c Release --runtime ubuntu.18.04-x64 -o /tmp/AgariTaku
ssh jinbijin@84.22.102.178 'pkill -x AgariTaku'
rsync -r --delete-after --quiet /tmp/AgariTaku/ jinbijin@84.22.102.178:~/AgariTaku
ssh -f jinbijin@84.22.102.178 './AgariTaku/AgariTaku'
echo 'Deployment successful!'
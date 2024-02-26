kubectl delete -f .\backservice-hello-dotnet-deployment.yaml
kubectl apply -f .\backservice-hello-dotnet-deployment.yaml
kubectl delete -f .\backservice-bye-nodejs-deployment.yaml
kubectl apply -f .\backservice-bye-nodejs-deployment.yaml
kubectl get pods -w

kubectl create configmap frontservice-dotnet-envoy-config --from-file=envoy.yaml --dry-run=client -o yaml | kubectl apply -f -
kubectl delete -f .\frontservice-dotnet-deployment.yaml
kubectl apply -f .\frontservice-dotnet-deployment.yaml
kubectl get pods -w

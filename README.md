Ref Issue: https://github.com/envoyproxy/envoy/issues/32580


Trying envoy proxy as sidecar in pods

Pre-requisites: Working Kubernetes cluster.

Follow these steps to setup
1)	Clone the repo
2)	Navigate to k8s folder
3)	If you are running minikube for kubernetes cluster, run the below command
`minikube tunnel`

5)	Run following commands to deploy the resources
```
kubectl apply -f .\backservice-hello-dotnet-deployment.yaml
kubectl apply -f .\backservice-hello-dotnet-service.yaml

kubectl apply -f .\backservice-bye-nodejs-deployment.yaml
kubectl apply -f .\backservice-bye-nodejs-service.yaml
kubectl create configmap frontservice-dotnet-envoy-config --from-file=envoy.yaml --dry-run=client -o yaml | kubectl apply -f -
kubectl apply -f .\frontservice-dotnet-deployment.yaml
kubectl apply -f .\frontservice-dotnet-service

```

5)	Get url to invoke service
`kubectl get svc`

6) Take note of the exposed `frontservice-dotnet`  and open in browser, navigate to *hello* or *bye* to invoke respective backend services via the frontend. These requests will go through the envoy proxy running inside pods of frontservice-dotnet app.
   for example: `http://localhost:5001/hello` or `http://localhost:5001/bye`
   Note that backend services are configured to alwyas throw error, retry from envoy
   

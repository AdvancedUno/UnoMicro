kubectl apply -f uno-depl.yaml 
kubectl apply -f uno-np-srv.yaml

kubectl get deployments
kubectl get services

kubectl rollout restart deployment uno-depl 


kubectl get pods
kubectl get namespace


kubectl get pods --namespace=ingress-nginx
kubectl get services --namespace=ingress-nginx

kubectl create secret generic mssql --from-literal=SA_PASSWORD="pa55w0rd!" 

sa
localhost,1433 for sqlserver

### Delete deployment
kubectl get deployment
kubectl delete deployment [name of pod]

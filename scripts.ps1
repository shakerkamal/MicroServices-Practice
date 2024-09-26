kubectl apply -f .\platform-depl.yaml
kubectl apply -f .\platform-nodeport-srv.yaml
kubectl apply -f .\comman-depl.yaml

# for applying ingress controller
kubectl apply -f .\ingress-srv.yaml


# check deploymetns
kubectl get deployments

# check services
kubectl get services

# check pods
kubectl get po


# check pods, services of ingress contriller
kubectl get services --namespace=ingress-controller
kubectl get po --namespace=ingress-controller
# Distancia entre 2 ceps

## Aqui utilizei de 2 APIs, ViaCep e Nominatim para medir a distancia entre 2 ceps

Primeiro utiliza da ViacCep para poder encontrar informações sobre o cep, Rua,Bairro,etc...
Logo apos utiliza da Nominatim para transformar essas informações em latitude e longitude e volto para o codigo principal utilizado da formula de haversine para medir a distancia dos 2 ceps

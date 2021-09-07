document.getElementById('btnSubmit')
    .addEventListener('click', function (e) {
        e.preventDefault();

        handleSubmitAsync();
    });

document.getElementById('url')
    .addEventListener('keyup', function (evt) {
        if (evt.code === 'Enter') {
            event.preventDefault();
            handleSubmitAsync();
        }
    });

function handleSubmitAsync() {
    const url = document.getElementById('url').value;

    var json = { 'url': url };

    const headers = {
        'content-type': 'application/json'
    };

    fetch('/urls', { method: 'post', body: JSON.stringify(json), headers: headers })
        //.then(apiResult => apiResult.json())
        .then(apiResult => {
            return new Promise(resolve => apiResult.json()
                .then(json => {
                    return resolve({ ok: apiResult.ok, status: apiResult.status, json: json })
                })
            );
        })
        .then(({ json, ok, status }) => {
            console.log(json);

            if (ok) {

                const anchor = `<a href=${json.shortUrl} target="_blank">${json.shortUrl}</a>`;

                document.getElementById('urlResult').innerHTML = anchor;
            }
            else {
                alert(json.errorMessage);
            }
        });
}
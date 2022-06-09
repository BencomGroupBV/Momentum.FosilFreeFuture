async function metaMaskButtonClick() {
    if (typeof window.ethereum !== "undefined") {
        const accounts = await ethereum.request({ method: "eth_requestAccounts" })
        var participantWalletAddress = document.getElementById("participantWalletAddress");
        participantWalletAddress.value = accounts[0];
        var button = document.getElementById("btnCreateProject");
        button.disabled = false;
        button.title = "";
    }
    else {
        alert("Please install the MetaMask extension for your browser.");
        window.open("https://metamask.io/download/", "_blank");
    }
}
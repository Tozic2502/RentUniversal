

function Footer() {
    return (
        <footer className="footer">
            {/* Logo til venstre */}
            <div>
                <img
                    src="/Logo.jpg"
                    alt="RentAll logo"
                    className="footer-logo-img"
                />
            </div>

            {/* Tekstbokse til højre */}
            <div className="footer-links">
                <div className="footer-box">
                    <h4>Om os</h4>
                    <p>
                        RentAll er en udlejningsplatform, der samler forskelligt udstyr ét sted –
                        fra værktøj og udendørsudstyr til festartikler. Du betaler kun for det,
                        du bruger, når du har brug for det.
                    </p>
                </div>

                <div className="footer-box">
                    <h4>Privatliv</h4>
                    <p>
                        Vi behandler dine personoplysninger fortroligt og bruger dem kun til
                        at håndtere dine bookinger, betalinger og din konto. Alle oplysninger bliver håndteret efter GDPR.
                        Du kan altid kontakte os for indsigt, rettelse eller tilbagekaldelse af Samtykkeerklæring.
                    </p>
                </div>

                <div className="footer-box">
                    <h4>Support</h4>
                    <p>
                        Har du spørgsmål vedrørende en booking eller udfordringer med din konto, er du velkommen til at kontakte vores support via formularen ovenfor.

                    </p>
                </div>
            </div>
        </footer>
    );
}

export default Footer;